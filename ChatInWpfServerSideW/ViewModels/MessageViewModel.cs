using ChatInWpfServerSideW.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatInWpfServerSideW.ViewModels
{
   public class MessageViewModel:BaseViewModel
    {
        public MessageViewModel()
        {
            CurrentMessage = new Message();
        }
        private ObservableCollection<Message> allMessages;
        public ObservableCollection<Message> AllMessages
        {
            get
            {
                return allMessages;
            }
            set
            {
                allMessages = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(AllMessages)));
            }
        }
        private Message currentMessage;
        public Message CurrentMessage
        {
            get
            {
                return currentMessage;
            }
            set
            {
                currentMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CurrentMessage)));
            }
        }
    }
}
