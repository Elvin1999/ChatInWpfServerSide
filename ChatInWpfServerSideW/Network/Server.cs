using ChatInWpfServerSideW.Entities;
using ChatInWpfServerSideW.ViewModels;
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
        public List<Message> AllMessages { get; set; }

        public void StartProcess()
        {
            #region Client Message
            Task sender = Task.Run(() =>
            {
                // string message = Console.ReadLine();
              
                                        while (true)
                                        {
  App.Current.Dispatcher.Invoke(() =>
                                    {
                                            if (Message != String.Empty)
                                            {
                                                if (Message == "quit")
                                                {
                                                    foreach (var item in App.clients)
                                                    {
                                                        item.Shutdown(SocketShutdown.Send);
                                                    }
                                                }
                                                else
                                                {
                                                    Task ss = Task.Run(() =>
                                    {
                                                    foreach (var item in App.clients)
                                                    {
                                                        if (Message != String.Empty && Message != null)
                                                        {
                                            //MessageBox.Show(Message);
                                            item.Send(Encoding.ASCII.GetBytes(Message));
                                                            Message = String.Empty;
                                                        }
                                        //                    App.Current.Dispatcher.Invoke(() =>
                                        //{

                                        //    MessageViewModel.AllMessages.Add(item);
                                        //});
                                    }
                                                });
                                                }
                                            }
 });
                                        }
                                   
            });

            Task receiver = Task.Run(() =>
            {
                while (true)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        Task ss1 = Task.Run(() =>
                                        {
                                            try
                                            {

                                                int length = App.client.Receive(buffer);
                                                var client_message = Encoding.ASCII.GetString(buffer, 0, length);
                                                if (client_message != String.Empty)
                                                {
                                                    MessageBox.Show(client_message);
                                                    var item = new Message();
                                                    item.DateTime = DateTime.Now;
                                                    item.Id = 1;
                                                    item.Text = client_message;
                                                    App.Server.Message = item.Text + "   " + item.DateTime.ToLongTimeString();

                                                    App.Current.Dispatcher.Invoke(() =>
                                                    {
                                                        if (item.Text != String.Empty)
                                                            App.Server.AllMessages.Add(item);
                                                    });


                                                }
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        });
                    });

                }
            });
            Task.WaitAll(sender, receiver);
            #endregion      
        }
    }
}
