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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        private BusinessInterface foob;
        public Main()
        {
            InitializeComponent();
            InitializeComponent();
            //The below codes are used to access the business tier interface
            ChannelFactory<BusinessInterface> foobFactory;

            //creates a client stub
            NetTcpBinding tcp = new NetTcpBinding();

           
            var URL = "net.tcp://localhost:50002/BankBiz";
            foobFactory = new ChannelFactory<BusinessInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }
        String firstname, lastname;
        uint id;

        //when cehck button is seleted
        private void Button_Click_check(object sender, RoutedEventArgs e)
        {
            id = Convert.ToUInt32(searchID.Text);

            //search whether id exist in the file, if it exists, it is added to the list
            uint result = foob.SearchUserAccount(id);
            if (result != 0)
            {
                userList.Items.Add(result);
                
            }
            else
            {
                //if it doest exist, this message will be displayed
                userList.Items.Add("Your user account doesn't exist!");
            }
        }

        uint newID;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            firstname = fname.Text;
            lastname = lname.Text;
            newID= foob.CreateUserAccount(firstname, lastname);
            userID.Content = newID;
           // userList.Items.Add(newID);
            //accnt_btn.IsEnabled = true;
           
        }

        private void accnt_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            //the userid is passed to the next page
            nav.Navigate(new Account(id));
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Main.xaml", UriKind.Relative));
        }

        //this function is written to navigate to the next page if a list item is selected
        private void listView_Click(object sender, RoutedEventArgs e)
        {
            accnt_btn.IsEnabled = true;
               //the account button is enabled only if user selects an acount
        }


    }
}
