using aka_crm.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aka_crm.ViewModels
{
    class CustomerViewModel
    {
        private SqlConnection getConnection()
        {
            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "DESKTOP-HBKMI7B\\SQLEXPRESS";
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "aka-crm";

                return new SqlConnection(builder.ConnectionString);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        public Customer getById(int id)
        {
            Customer customer = new Customer();

            using (SqlConnection connection = getConnection())
            {
                connection.Open();

                SqlCommand sql = new SqlCommand();
                String query = "SELECT * FROM Company WHERE id = @id";
                sql = new SqlCommand(query, connection);
                sql.Parameters.AddWithValue("@id", id.ToString());
                SqlDataReader reader = sql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customer.Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Id")).ToString());
                        customer.Name = reader.GetValue(reader.GetOrdinal("name")).ToString();
                        customer.Created = DateTime.Parse(reader.GetValue(reader.GetOrdinal("created")).ToString());
                        customer.Modified = DateTime.Parse(reader.GetValue(reader.GetOrdinal("modified")).ToString());
                        return customer;
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            }
            return null;

        }

        public CustomerProfile getProfile(Customer customer)
        {
            CustomerProfile profile = new CustomerProfile();

            using (SqlConnection connection = getConnection())
            {
                connection.Open();

                SqlCommand sql = new SqlCommand();
                String query = "SELECT * FROM Profile WHERE customerId = @customerId";
                sql = new SqlCommand(query, connection);
                sql.Parameters.AddWithValue("@customerId", customer.Id);
                SqlDataReader reader = sql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        profile.ResponsibleParty = reader.GetValue(reader.GetOrdinal("ResponsibleParty")).ToString();
                        profile.Street = reader.GetValue(reader.GetOrdinal("Street")).ToString();
                        profile.City = reader.GetValue(reader.GetOrdinal("City")).ToString();
                        profile.State = reader.GetValue(reader.GetOrdinal("State")).ToString();
                        profile.Zip = reader.GetValue(reader.GetOrdinal("Zip")).ToString();
                        profile.Phone = reader.GetValue(reader.GetOrdinal("Phone")).ToString();
                        profile.Email = reader.GetValue(reader.GetOrdinal("Email")).ToString();

                        return profile;
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            }
            return null;

        }

        public List<Customer> getAll()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = getConnection())
            {
                connection.Open();

                SqlCommand sql = new SqlCommand();
                String query = "SELECT * FROM Company";
                sql = new SqlCommand(query, connection);
                SqlDataReader reader = sql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Id")).ToString());
                        customer.Name = reader.GetValue(reader.GetOrdinal("name")).ToString();
                        customer.Created = DateTime.Parse(reader.GetValue(reader.GetOrdinal("created")).ToString());
                        customer.Modified = DateTime.Parse(reader.GetValue(reader.GetOrdinal("modified")).ToString());
                        customers.Add(customer);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            }
            return customers;
        }

        public void addCustomer(Customer customer, CustomerProfile profile)
        {
            using (SqlConnection connection = getConnection())
            {
                connection.Open();

                SqlCommand sql = new SqlCommand();
                String query = "INSERT INTO Company (name, created, modified) output INSERTED.ID VALUES (@name, @created, @modified)";
                sql = new SqlCommand(query, connection);
                sql.Parameters.AddWithValue("@name", customer.Name);
                sql.Parameters.AddWithValue("@created", DateTime.Now);
                sql.Parameters.AddWithValue("@modified", DateTime.Now);
                int customerId = (int) sql.ExecuteScalar();
                Console.WriteLine(customerId);

                query = "INSERT INTO Profile (customerId, responsibleParty, street, city, state, zip, phone, email) output INSERTED.ID VALUES (@customerId, @responsibleParty, @street, @city, @state, @zip, @phone, @email)";
                sql = new SqlCommand(query, connection);
                sql.Parameters.AddWithValue("@customerId", customerId);
                sql.Parameters.AddWithValue("@responsibleParty", profile.ResponsibleParty);
                sql.Parameters.AddWithValue("@street", profile.Street);
                sql.Parameters.AddWithValue("@city", profile.City);
                sql.Parameters.AddWithValue("@state", profile.State);
                sql.Parameters.AddWithValue("@zip", profile.Zip);
                sql.Parameters.AddWithValue("@phone", profile.Phone);
                sql.Parameters.AddWithValue("@email", profile.Email);
                sql.ExecuteNonQuery();

            }
        }
    }
}
