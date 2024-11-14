using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Client
    {
        public string ID
        {
            get;
            private set;
        }



        public IPEndPoint EndPoint
        {
            get;
            private set;
        }

        public string clientName
        {
            get;
            private set;
        }

   


        public void setName(string name)
        {
            clientName = name + " ";
        }

        public Socket sck;
        public Client(Socket accepted)
        {
            sck = accepted;
            ID = Guid.NewGuid().ToString();
            EndPoint = (IPEndPoint)sck.RemoteEndPoint;
            sck.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
        }

        void callback(IAsyncResult ar)
        {
            try
            {
                sck.EndReceive(ar);

                byte[] buf = new byte[8192];

                int rec = sck.Receive(buf, size: buf.Length, 0);

                if (rec < buf.Length)
                {
                    Array.Resize<byte>(ref buf, rec);
                }

                string receivedData = Encoding.Default.GetString(buf);

                // If this is the first message, treat it as the client's name
                if (string.IsNullOrEmpty(clientName))
                {
                    clientName = receivedData;
                    //Console.WriteLine($"Client's name: {clientName}"); // Store or log the client's name
                }
                //else
                //{
                //    Console.WriteLine($"Message from client: {receivedData}");  // Handle subsequent messages
                //}

                if (Received != null)
                {
                    Received(this, buf);
                }

                sck.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                close();

                if (Disconnected != null)
                {
                    Disconnected(this);
                }


            }
        }

        public void close()
        {
            sck.Close();
            sck.Dispose();
        }

        public delegate void ClientReceivedHandler(Client sender, byte[] data);
        public delegate void ClientDisconnectedHandler(Client sender);

        public event ClientReceivedHandler Received;
        public event ClientDisconnectedHandler Disconnected;
    }
}
