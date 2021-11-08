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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        private BusinessInterface foob;
        List<uint> finalList = new List<uint>();
        public Admin(List<uint> transList)
        {
            InitializeComponent();
            //retrieving the list of transactions passed from transaction.xaml
            finalList = transList;

            ChannelFactory<BusinessInterface> foobFactory;

            //creates a client stub
            NetTcpBinding tcp = new NetTcpBinding();

           
            var URL = "net.tcp://localhost:50002/BankBiz";
            foobFactory = new ChannelFactory<BusinessInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();


        }
        
        //is executed when user presses load button of the transactions
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //adding all the values of the transaction list to the list view
            foreach (uint element in finalList)
            {
                transList.Items.Add(element);
               

            }
        }

       //is executed when user presses load button of the users
        private void user_Click(object sender, RoutedEventArgs e)
        {
            //retrieving all users List and adding them to the list view
            foreach (uint element in foob.GetAllUsers())
            {
                allUserList.Items.Add(element);
              

            }
        }

        //when a user id from the list view is selected
        private void myListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //To convert the details of the selected item to string and store it in a variable called value
            uint value = (uint)allUserList.SelectedItem;
            string[] output = new string[2];
            
            //get the user details by passing user id
            output = foob.GetSpecificUserDetails(value);
            String result = "FirstName: " + output[0] + " LastName: " + output[1];
            //display the result in a message box
            MessageBox.Show(result, "Note!");
        }

        //when processalltransaction button is clicked
        private void process_Click(object sender, RoutedEventArgs e)
        {
            foob.ProcessTransactions(finalList);
            transList.Items.Clear();
        }
    }
}
