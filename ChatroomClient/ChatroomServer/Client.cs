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
        public string username = "";

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

            string data = Encoding.Unicode.GetString(FormattedBuffer);
            processData(data);
            clientSocket.BeginReceive(buffer, 0, BUFFERSIZE, SocketFlags.None, recieveData, null);
        }

        void processData(string data)
        {
            string[] splitData = data.Split(';');
            //Get the context of the data
            switch (splitData[0])
            {
                case "username":
                    string oldUsername = username;
                    username = splitData[1];
                    onUsernameChange(username,oldUsername);
                    break;

                case "message":
                    onDataRecieved(splitData[1],this);
                    break;
            }
        }

        public override string ToString()
        {
            return username;
        }

        public void disconnect()
        {
            clientSocket.Disconnect(false);
            onUsernameChange("", username);
        }

        public delegate void userNameChangeHandler(string username,string oldUsername);
        public event userNameChangeHandler onUsernameChange;

        public delegate void dataRecievedHandler(string data,Client client);
        public event dataRecievedHandler onDataRecieved;

    }
}
