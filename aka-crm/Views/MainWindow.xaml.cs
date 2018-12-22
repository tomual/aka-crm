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

            NewCustomerForm.Visibility = Visibility.Hidden;

            Customer customer;
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customer = customerViewModel.getById(16);
            displayProfile(customer);

            CustomerList.ItemsSource = customerViewModel.getAll();
        }

        private void loadList()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            CustomerList.ItemsSource = customerViewModel.getAll();
        }

        private void displayProfile(Customer customer)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            CustomerProfile profile;
            profile = customerViewModel.getProfile(customer);

            GridTitle.Text = customer.Name;

            NameText.Text = customer.Name;
            ResponsiblePartyText.Text = profile.ResponsibleParty;
            CreatedText.Text = customer.Created.ToString();
            ResponsiblePartyText.Text = profile.ResponsibleParty;
            StreetText.Text = profile.Street;
            CityStateZipText.Text = profile.City + " " + profile.State + " " + profile.Zip;
            PhoneText.Text = profile.Phone;
            EmailText.Text = profile.Email;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewCustomerForm.Visibility = Visibility.Visible;
            CustomerProfile.Visibility = Visibility.Hidden;
            GridTitle.Text = "New Customer";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer();
            CustomerProfile profile = new CustomerProfile();

            customer.Name = NameInput.Text;
            profile.Street = StreetInput.Text;
            profile.City = CityInput.Text;
            profile.State = StateInput.Text;
            profile.Zip = ZipInput.Text;
            profile.ResponsibleParty = ResponsiblePartyInput.Text;
            profile.Email = EmailInput.Text;
            profile.Phone = PhoneInput.Text;

            CustomerViewModel customerViewModel = new CustomerViewModel();
            customer.Id = customerViewModel.addCustomer(customer, profile);
            customer.Created = DateTime.Now;
            loadList();
            displayProfile(customer);

            NewCustomerForm.Visibility = Visibility.Hidden;
            CustomerProfile.Visibility = Visibility.Visible;
        }
    }
}
