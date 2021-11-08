using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for transfer.xaml
    /// </summary>
    public partial class transfer : Page
    {
        private BusinessInterface foob;
        uint account = 0;
        uint user = 0;
        public transfer(uint userID, uint accID)
        {
            InitializeComponent();
            account = accID;
            user = userID;
            accountID.Content = account;
            userIDNo.Content = user;

            //to access functions of business tier interface
            ChannelFactory<BusinessInterface> foobFactory;

            //creates a client stub
            NetTcpBinding tcp = new NetTcpBinding();

            //Set the URL and create the connection!string
            var URL = "net.tcp://localhost:50002/BankBiz";
            foobFactory = new ChannelFactory<BusinessInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }
        List<uint> trans = new List<uint>();

        private void transfer_Click(object sender, RoutedEventArgs e)
        {
           
          
            //to check whtehr inpur fields are empty
            if (string.IsNullOrEmpty(senderAcc.Text) || string.IsNullOrEmpty(recAcc.Text) || string.IsNullOrEmpty(recAcc.Text)) {
                MessageBox.Show("Please fill all the fields properly.", "Error!");
            }

            else {
                
                //to check whther input fields have proper interger values
                  if( (Regex.IsMatch(senderAcc.Text, @"^[0-9]+$") == false) || (Regex.IsMatch(recAcc.Text, @"^[0-9]+$") == false) ||(Regex.IsMatch(amount.Text, @"^[0-9]+$") == false)){
                    MessageBox.Show("Please enter only positive integers.", "Error!");
                    }

                 else {
                    uint senderAccount = Convert.ToUInt32(senderAcc.Text);
                    uint receiverAccount = Convert.ToUInt32(recAcc.Text);
                    uint fund = Convert.ToUInt32(amount.Text);

                    //transfer fund
                    String result = foob.transferFund(senderAccount, receiverAccount, fund);
                    output.Content = result;
                  

                    foreach (uint element in foob.GetTransactions())
                    {
                        transList.Items.Add(element);
                      //add all the transaction id to list which is passed to home page
                        trans.Add(element);
                    }
                }
            }
        }

        //when logout button is selected
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // this.NavigationService.Navigate(new Uri("Home.xaml", UriKind.Relative));

            NavigationService nav = NavigationService.GetNavigationService(this);
            //thetransaction list ids are passed to the home page
            nav.Navigate(new Home(trans));
        }
    }
}
