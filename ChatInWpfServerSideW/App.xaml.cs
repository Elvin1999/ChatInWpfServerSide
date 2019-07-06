using ChatInWpfServerSideW.Network;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace ChatInWpfServerSideW
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Server Server { get; set; }
        public static List<Socket> clients = new List<Socket>();
        public static Socket client = null;
      
        void ConnectToServer()
        {
            Server = new Server();
            IPEndPoint endp = new IPEndPoint(IPAddress.Parse("192.168.1.103"), 1031);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endp);
            socket.Listen(10);
            int counter = 0;
            Task accept = Task.Run(() =>
            {
                while (true)
                {
                    client = socket.Accept();
                    clients.Add(client);
                    ++counter;
                    if (counter == 1)
                    {
                        break;
                    }
                }
            });
        }
        public App()
        {
            Task connect = Task.Run(() =>
            {
                ConnectToServer();
            });
            connect.Wait();

            Task start = Task.Run(() =>
            {
                App.Server.StartProcess();
            });
        }
    }
}
