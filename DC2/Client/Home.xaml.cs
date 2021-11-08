using System;
using System.Collections.Generic;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {

        List<uint> transList = new List<uint>();
        public Home()
        {
            //if a transaction list is not passed
            InitializeComponent();
        }
        public Home(List<uint> trans)
        {
           //if a transaction list is passed from transaction page
           //the list is retreived and stored
            transList = trans;
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //navigate to users home page
            this.NavigationService.Navigate(new Uri("Main.xaml", UriKind.Relative));
        }

        private void admin_Checked(object sender, RoutedEventArgs e)
        {

            NavigationService nav = NavigationService.GetNavigationService(this);
            //the transaction list is navigated to admin page
            nav.Navigate(new Admin(transList));
        }
    }
}
