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
        public MessageViewModel MessageViewModel { get; set; }
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
                                                    item.Id = MessageViewModel.AllMessages.Count+1;
                                                    try
                                                    {
                                                        if (client_message != String.Empty)
                                                        {
                                                            App.Server.Message = client_message + "   " + item.DateTime.ToLongTimeString();
                                                            item.Text = App.Server.Message;
                                                            if (item.Text != String.Empty)
                                                            {
                                                                App.Current.Dispatcher.Invoke(() =>
                                                                {
                                                                    MessageViewModel.AllMessages.Add(item);
                                                                });
                                                            }
                                                        }

                                                    }
                                                    catch (Exception ex)
                                                    {

                                                        MessageBox.Show(ex.Message);
                                                    }


                                                }
                                            }
                                            catch (Exception ex)
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
