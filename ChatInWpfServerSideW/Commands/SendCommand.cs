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
            throw new NotImplementedException();
        }
    }
}
