using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Net;
using System.Net.Sockets;

namespace ChatroomClient
{
    /// <summary>
    /// Interaction logic for StartingWindow.xaml
    /// </summary>
    public partial class StartingWindow : Window
    {
        int port = 3878;

        public StartingWindow()
        {
            InitializeComponent();
        }

        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {
            IPAddress address;
            bool validIp = IPAddress.TryParse(txt_ip.Text,out address);
            if (validIp)
            {
                if (checkValidUsername(txt_username.Text))
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint endPoint = new IPEndPoint(address, port);
                    bool connected = tryConnect(socket, endPoint);
                    if (connected)
                    {
                        MainWindow client = new MainWindow(socket, txt_username.Text);
                        client.Show();
                        this.Hide();
                    }
                    else
                    {
                        lbl_error.Content = "Failed to connect";
                    }
                }
                else
                {
                    lbl_error.Content = "Please enter a valid username";
                }
            }
            else
            {
                lbl_error.Content = "Please enter a valid IP address";
            }
        }

        bool checkValidUsername(string username)
        {
            bool valid = true;

            if (String.IsNullOrWhiteSpace(username))
                valid = false;

            if (username.Contains(';'))
                valid = false;

            return valid;
        }

       bool tryConnect(Socket socket,IPEndPoint endPoint)
        {
            try
            {
                socket.Connect(endPoint);
            }
            catch
            {
                return false;
            }
            return true;
        }

     
    }
}
