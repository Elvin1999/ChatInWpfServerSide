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
            var item=MessageViewModel.CurrentMessage;
            item.DateTime = DateTime.Now;
            if (MessageViewModel.AllMessages.Count != 0)
                item.Id = MessageViewModel.AllMessages.Count - 1;
            else
                item.Id = 0;

            MessageViewModel.AllMessages.Add(item);
        }
    }
}
