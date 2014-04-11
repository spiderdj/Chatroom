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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net;
using System.Net.Sockets;

namespace ChatroomClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int BUFFERSIZE = 1024;
        Socket socket;
        byte[] buffer = new byte[BUFFERSIZE];

        string username;

        public MainWindow(Socket socket, string username)
        {
            InitializeComponent();
            this.socket = socket;
            this.username = username;
            sendString("username;" + username);
        }


        void sendString(string data)
        {
            byte[] byteData = Encoding.Unicode.GetBytes(data);
            socket.Send(byteData);
        }

    }
}
