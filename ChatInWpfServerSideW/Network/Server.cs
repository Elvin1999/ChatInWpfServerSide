using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatInWpfServerSideW.Network
{
   public class Server
    {
        byte[] buffer = new byte[1024];
        public string Message { get; set; }
        public  void AcceptCallback(IAsyncResult ia)
        {
            Socket socket = (Socket)ia.AsyncState;
            socket = socket.EndAccept(ia);
            Task.Run(() =>
            {
                while (true)
                {
                   // string message = Console.ReadLine();
                    if (Message == "quit")
                    {
                        socket.Shutdown(SocketShutdown.Send);
                        break;
                    }
                    else
                    {
                        socket.Send(Encoding.ASCII.GetBytes(Message));
                    }
                    //Console.WriteLine("Server 1 " + message);
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    int length = socket.Receive(buffer);
                    //Console.WriteLine("Client  " + Encoding.ASCII.GetString(buffer, 0, length));
                }
            });
        }
        public  void StartServer()
        {
            byte[] buffer = new byte[1024];
            IPEndPoint endp = new IPEndPoint(IPAddress.Parse("192.168.1.103"), 1031);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endp);
            socket.Listen(10);
            socket.BeginAccept(new AsyncCallback(AcceptCallback), socket);
        }
        //==========================================
        public  void Send(Socket client)
        {
            while (true)
            {
                //string message = Console.ReadLine();               
                client.Send(Encoding.ASCII.GetBytes(Message));
                //Console.WriteLine("Server 1 " + message);
            }
        }
        //===========================================
        public  void Receive(Socket client)
        {
            while (true)
            {
                byte[] buffer = new byte[256];
                int length = client.Receive(buffer);
                //Console.WriteLine("Client 1 " + Encoding.ASCII.GetString(buffer, 0, length));
            }
        }

        public void StartProcess()
        {
            #region Client Message
            IPEndPoint endp = new IPEndPoint(IPAddress.Parse("192.168.1.103"), 1031);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endp);
            socket.Listen(10);
            byte[] buffer = new byte[1024];
            List<Socket> clients = new List<Socket>();
            int counter = 0;
            Socket client = null;
            Task accept = Task.Run(() =>
            {
                while (true)
                {
                    client = socket.Accept();
                    clients.Add(client);
                    ++counter;
                    if (counter == 2)
                    {
                        break;
                    }
                }

            });
            Task sender = Task.Run(() =>
            {
                while (true)
                {
                   // string message = Console.ReadLine();
                    if (Message == "quit")
                    {
                        foreach (var item in clients)
                        {

                            item.Shutdown(SocketShutdown.Send);
                        }
                        break;
                    }
                    else
                    {
                        Task ss = Task.Run(() =>
                        {
                            foreach (var item in clients)
                            {
                                item.Send(Encoding.ASCII.GetBytes(Message));
                            }
                        });
                    }
                    //Console.WriteLine("Server 1 " + message);
                }
            });
            Task receiver = Task.Run(() =>
            {
                while (true)
                {

                    Task ss1 = Task.Run(() =>
                    {
                        try
                        {

                            int length = client.Receive(buffer);
                            //Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, length));


                        }
                        catch (Exception)
                        {
                        }
                    });


                }

            });
            Task.WaitAll(sender, receiver);
            #endregion
        
        }
    }
}
