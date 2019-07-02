using ChatInWpfServerSideW.Entities;
using ChatInWpfServerSideW.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatInWpfServerSideW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MessageViewModel messageViewModel = new MessageViewModel();
            messageViewModel.AllMessages = new ObservableCollection<Message>() {

                new Message(){
                    Id=1, DateTime=DateTime.Now, Text="Salam salam salam \n salam salam salam"
                },
                new Message(){
                    Id=2, DateTime=DateTime.Now, Text="Salam salam salam  salam salam salam"
                },
                new Message(){
                    Id=3, DateTime=DateTime.Now, Text="Salam salam salam  salam salam salam"
                },
                new Message(){
                    Id=4, DateTime=DateTime.Now, Text="Salam salam salam  salam salam salam"
                },
                new Message(){
                    Id=5, DateTime=DateTime.Now, Text="Salam salam salam  salam salam salam"
                }

            };
            DataContext = messageViewModel;

        }
    }
}
