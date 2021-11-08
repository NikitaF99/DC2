using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusinessInterface foob;
        public MainWindow()
        {
            InitializeComponent();
            //The below codes are used to access the server interface
            ChannelFactory<BusinessInterface> foobFactory;

            //creates a client stub
            NetTcpBinding tcp = new NetTcpBinding();

            //Set the URL and create the connection!string
            var URL = "net.tcp://localhost:50002/BankBiz";
            foobFactory = new ChannelFactory<BusinessInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }
        String firstname, lastname;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            firstname = fname.Text;
            lastname = lname.Text;

            userID.Content=foob.CreateUserAccount(firstname, lastname);
        }
    }
}
