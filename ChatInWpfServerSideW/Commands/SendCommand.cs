﻿using ChatInWpfServerSideW.Entities;
using ChatInWpfServerSideW.Network;
using ChatInWpfServerSideW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace ChatInWpfServerSideW.Commands
{
    public class SendCommand : ICommand
    {
        public SendCommand(MessageViewModel messageViewModel)
        {
            MessageViewModel = messageViewModel;
        }

        public event EventHandler CanExecuteChanged;
        public MessageViewModel MessageViewModel { get; set; }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var item = MessageViewModel.CurrentMessage;
            item.DateTime = DateTime.Now;
            if (MessageViewModel.AllMessages.Count != 0)
                item.Id = MessageViewModel.AllMessages.Count;
            else
                item.Id = 0;
            if (item.Text != String.Empty)
                App.Server.Message = item.Text + "   " + item.DateTime.ToLongTimeString();
            // Task add = Task.Run(() =>
            //  {
            App.Current.Dispatcher.Invoke(() =>
                          {
                              if (item.Text != String.Empty)
                                  MessageViewModel.AllMessages.Add(item);
                          });
            //    });
            MessageViewModel.CurrentMessage = new Message();
        }
    }
}
