﻿using ChatInWpfServerSideW.Entities;
using ChatInWpfServerSideW.Network;
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
            messageViewModel.AllMessages = new ObservableCollection<Message>();
            //App.Server = new Server();
            App.Server.AllMessages = new List<Message>(messageViewModel.AllMessages);
            DataContext = messageViewModel;

        }
    }
}
