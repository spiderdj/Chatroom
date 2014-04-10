using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace ChatroomServer
{
    class Client
    {
        Socket clientSocket;
        string username = "";

        const int BUFFERSIZE = 1024;

        byte[] buffer = new byte[BUFFERSIZE];

        public Client(Socket socket)
        {
            //Get the client socket and tell it to start listening for data
            clientSocket = socket;
            clientSocket.BeginReceive(buffer, 0, BUFFERSIZE, SocketFlags.None, recieveData, null);

        }

        public void sendString(string data)
        {
            //Get the string in byte form and try to send it
            byte[] bytesToSend = Encoding.Unicode.GetBytes(data);
            try
            {
                clientSocket.Send(bytesToSend);
            }
            catch
            {
                //ToDo Handle unexpected client disconnect
            }
        }

        void recieveData(IAsyncResult IR)
        {
            //Recieved data in the buffer
            int amountOfData = clientSocket.EndReceive(IR);

            byte[] FormattedBuffer = new byte[amountOfData];
            Array.Copy(buffer, FormattedBuffer, amountOfData);
            //ToDo do something with the data
            clientSocket.BeginReceive(buffer, 0, BUFFERSIZE, SocketFlags.None, recieveData, null);
        }

    }
}
