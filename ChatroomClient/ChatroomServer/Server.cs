using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

namespace ChatroomServer
{
    public partial class frm_server : Form
    {
        int port = 3878;
        string roomName = "";

        Socket serverSocket;
        Dictionary<int, Client> clients = new Dictionary<int, Client>();


      
        public frm_server(string roomName)
        {
            InitializeComponent();
            //Set the room name
            this.roomName = roomName;
            this.Text = this.Text += "-" + roomName;
          
            InitalizeSocket();
            UpdateTimer.Start();
        }

        void InitalizeSocket()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, port);
            serverSocket.Bind(ipEnd);
            serverSocket.Listen(5);
            serverSocket.BeginAccept(onClientConnect,null);
        }


        void onClientConnect(IAsyncResult IR)
        {
            Socket clientSocket = (Socket)serverSocket.EndAccept(IR);
            serverSocket.BeginAccept(onClientConnect, null);

            Client client = new Client(clientSocket);
            client.onUsernameChange += onUsernameChange;
            client.onDataRecieved += dataRecieved;
            int hash = clientSocket.GetHashCode();
            clients.Add(hash, client);
         
        }



        void onUsernameChange(string username,string oldUsername="")
        {
            if (list_Clients.InvokeRequired) //Call thread safe upadte of username list
            {
                updateListBoxCallback d = new updateListBoxCallback(updateListBox);
                list_Clients.Invoke(d,new object[]{username,oldUsername});
            }
            else
            {
                list_Clients.Items.Remove(oldUsername);
                if(username != "")
                 list_Clients.Items.Add(username);
            }
        }

        delegate void updateListBoxCallback(string username, string oldUsername);

        void updateListBox(string username, string oldUsername)
        {
            list_Clients.Items.Remove(oldUsername);
            list_Clients.Items.Add(username);
        }

        void dataRecieved(string data, Client client)
        {
            foreach (int id in clients.Keys)
            {
                clients[id].sendString("message;"+client.username+":"  + data);
            }
        }


        bool shouldRemove(string username)
        {
            return true;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            list_Clients.Update();
            lbl_number.Text = clients.Keys.Count.ToString();
        }

        private void selectedItemChanged(object sender, EventArgs e)
        {
            if ((string)list_Clients.SelectedItem != "")
            {
                btn_kick.Enabled = true;
            }
            else
                btn_kick.Enabled = false;
        }

        private void btn_kick_Click(object sender, EventArgs e)
        {
            string selectedUsername = (string)list_Clients.SelectedItem;
            int selectedId = getClientWithUsername(selectedUsername);
            clients[selectedId].disconnect();
            clients.Remove(selectedId);
        }

        int getClientWithUsername(string username)
        {
            foreach (int id in clients.Keys)
            {
                if (clients[id].username == username)
                {
                    return id;
                }
            }
            return -1;
        }




    }
}
