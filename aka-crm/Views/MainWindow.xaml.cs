using aka_crm.Models;
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
            Console.WriteLine("Hello");
            InitializeComponent();

            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "DESKTOP-HBKMI7B\\SQLEXPRESS";
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "aka-crm";

                // Connect to SQL
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    SqlCommand sql = new SqlCommand();
                    connection.Open();

                    String query = "INSERT INTO Company (name, created, modified) VALUES (@name, @created, @modified)";
                    sql = new SqlCommand(query, connection);
                    sql.Parameters.AddWithValue("@name", "Steve's Shoe Shop");
                    sql.Parameters.AddWithValue("@created", DateTime.Now);
                    sql.Parameters.AddWithValue("@modified", DateTime.Now);
                    sql.ExecuteNonQuery();

                    sql.Connection = connection;
                    sql.CommandText = "SELECT * FROM Company";
                    sql.CommandType = System.Data.CommandType.Text;
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}\t{1}", reader.GetInt32(0),
                                reader.GetString(1));
                            CompanyName.Text = reader.GetString(1);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }


                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}
