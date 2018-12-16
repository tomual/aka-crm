using aka_crm.Models;
using aka_crm.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace aka_crm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Customer customer;
            CustomerProfile profile;
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customer = customerViewModel.getById(16);
            profile = customerViewModel.getProfile(customer);

            CustomerName.Text = customer.Name;
            Console.WriteLine(customer.Name);
            Console.WriteLine(profile.ResponsibleParty);

            CustomerList.ItemsSource = customerViewModel.getAll();

            customer = new Customer();
            customer.Name = "La Casa";

            profile = new CustomerProfile();
            profile.ResponsibleParty = "George Tan";
            profile.Street = "1900 Harney Street";
            profile.City = "Lincoln";
            profile.State= "NE";
            profile.Zip= "68102";
            profile.Phone = "402 231 2543";
            profile.Email = "testdude@mail.com";

            customerViewModel.addCustomer(customer, profile);
        }
    }
}
