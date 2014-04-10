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

        public frm_server(string roomName)
        {
            InitializeComponent();
            //Set the room name
            this.roomName = roomName;
            this.Text = this.Text += "-" + roomName;
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
            //toDo register client
        }


    }
}
