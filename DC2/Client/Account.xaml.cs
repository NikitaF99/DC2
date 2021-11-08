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
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        private BusinessInterface foob;
        uint userID = 0;
        uint accID = 0;
        public Account(uint id)
        {
            InitializeComponent();
            userId.Content = id;
            //save the user id passed from the previous interface
            userID = id;

            //to access business tier interface
            ChannelFactory<BusinessInterface> foobFactory;

            //creates a client stub
            NetTcpBinding tcp = new NetTcpBinding();

          
            var URL = "net.tcp://localhost:50002/BankBiz";
            foobFactory = new ChannelFactory<BusinessInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //when user selects list my account button, all the accounts will be listed to the user in a list
            foreach (uint element in foob.GetAccountIDsByUser(userID))
            {
                accountList.Items.Add(element);
          

            }
        }
        private void myListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //when user selects a respective account in the listview, the account id will be retireved and stored in accID.
            accID = (uint)accountList.SelectedItem;
        }

        uint balanceJson = 0;
        private void listView_Click(object sender, RoutedEventArgs e)
        {
           //when the user selects an account, the balance of the account will be displayed
            balanceJson = foob.GetAccountBalance(accID);
            balance.Content = balanceJson;

            //all the otehr buttons will be enabled
            deposit.IsEnabled = true;
            withdraw.IsEnabled = true;
            transfer.IsEnabled = true;
        }

        private void deposit_Click(object sender, RoutedEventArgs e)
        {
           

            if (string.IsNullOrEmpty(DepAmnt.Text))       //Checking whether the amount field is empty
            {
                MessageBox.Show("Please enter an amount.", "Error!");
            }
            else
            {
     
              
                //to check whether only numbers were entered as amount
                if (Regex.IsMatch(DepAmnt.Text, @"^[0-9]+$"))       
                {

                    //if the entered amount is uptoo expected criteria, amount will be deposited to the user's selected account
                    uint amount = Convert.ToUInt32(DepAmnt.Text);

                    uint bal = foob.Deposit(accID, amount);
                    MessageBox.Show("Successfully deposited to " + accID, "Success!");

                    bal = bal + amount;

                    //the updated balance is displayed to the user
                    balance.Content = bal;
                    balanceJson = bal;
                }

                //if interger values are not enterd

                if (!Regex.IsMatch(DepAmnt.Text, @"^[0-9]+$"))
                {
                   
                 
                    
                        MessageBox.Show("Please enter only positive integer values.", "Invalid amount!");
                    
                }
            }
        }

        private void withdraw_Click(object sender, RoutedEventArgs e)
        {
            // uint amount = Convert.ToUInt32(withAmnt.Text);
            //foob.Withdraw(accID, amount);

            if (string.IsNullOrEmpty(withAmnt.Text))       //Checking whether the amount field is empty
            {
                MessageBox.Show("Please enter an amount.", "Error!");
            }
            else
            {
              

                //to check whether only numbers were entered as amount

                if (Regex.IsMatch(withAmnt.Text, @"^[0-9]+$"))        
                {
                    //if the entered amount is uptoo expected criteria, amount will be withdrawn from the user's selected account
                    uint amount = Convert.ToUInt32(withAmnt.Text);

                    foob.Withdraw(accID, amount);
                    MessageBox.Show("Successfully withdrawed from " + accID, "Success!");


                    //show the updated balance to the user
                    balanceJson = balanceJson - amount;
                    
                    balance.Content = balanceJson;
                }


                if (!Regex.IsMatch(withAmnt.Text, @"^[0-9]+$"))
                {
                   

                    
                        MessageBox.Show("Please enter integer values.", "Invalid value!");
                    
                }
            }
        }

        //to navigate to transfer page on button click "transfer"
        private void transfer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            //the userID and accountID are passed to the next page
            nav.Navigate(new transfer(userID , accID));
        }

        //to create a new account on button click
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            accountList.Items.Add(foob.CreateAccount(userID));
        }

        /*
        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            uint bal = foob.GetAccountBalance(accID);

            //NavigationService nav = NavigationService.GetNavigationService(this);
            //nav.Navigate(new Account(userID));
        }*/


    }
}
