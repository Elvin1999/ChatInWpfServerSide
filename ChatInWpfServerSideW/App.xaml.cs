using ChatInWpfServerSideW.Network;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
        public App()
        {
            Server = new Server();
        }
    }
}
