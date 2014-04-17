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

using System.Threading;

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
            socket.BeginReceive(buffer, 0, BUFFERSIZE, SocketFlags.None, onDataRecieved, null);
        }


        void sendString(string data)
        {
            try
            {
                byte[] byteData = Encoding.Unicode.GetBytes(data);
                socket.Send(byteData);
            }
            catch
            {
                socket.Disconnect(false);
                onDisconnect();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            sendString("message;"+txt_message.Text);
            txt_message.Text = "";
        }


        void onDataRecieved(IAsyncResult IR)
        {
            int lengthRecieved = socket.EndReceive(IR);
            byte[] FormattedBuffer = new byte[lengthRecieved];
            Array.Copy(buffer, FormattedBuffer, lengthRecieved);
            string data = Encoding.Unicode.GetString(FormattedBuffer);
            processData(data);
            socket.BeginReceive(buffer, 0, BUFFERSIZE, SocketFlags.None, onDataRecieved, null);
        }

        void onDisconnect()
        {

        }


        delegate void addTextCallback(string text);

        void setText(string text)
        {
            txt_Output.AppendText( Environment.NewLine + text);
        }

        void processData(string data)
        {
            string[] split = data.Split(';');

            switch (split[0])
            {
                case "message":
                    addTextCallback d = new addTextCallback(setText);
                    txt_Output.Dispatcher.Invoke(d,new object[]{split[1]}); //Make a thread safe call to update the textbox
                    break;


            }
        }
    }
}
