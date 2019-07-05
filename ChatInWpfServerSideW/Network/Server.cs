using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace ChatInWpfServerSideW.Network
{
    public class Server
    {
        byte[] buffer = new byte[1024];
        public string Message { get; set; }
        public void StartProcess()
        {
            #region Client Message
            IPEndPoint endp = new IPEndPoint(IPAddress.Parse("10.1.16.33"), 1031);
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

                // string message = Console.ReadLine();
                while (true)
                {
                    if (Message != String.Empty)
                    {
                        if (Message == "quit")
                        {
                            foreach (var item in clients)
                            {
                                item.Shutdown(SocketShutdown.Send);
                            }

                        }
                        else
                        {
                            Task ss = Task.Run(() =>
                            {
                                foreach (var item in clients)
                                {
                                    item.Send(Encoding.ASCII.GetBytes(Message));
                                    Message = String.Empty;
                                }
                            });
                        }
                    }

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
                                            var client_message = Encoding.ASCII.GetString(buffer, 0, length);
                                            MessageBox.Show(client_message);
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
