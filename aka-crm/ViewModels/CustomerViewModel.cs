﻿using aka_crm.Models;
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

        public void addCustomer()
        {
            SqlConnection connection = getConnection();
            SqlCommand sql = new SqlCommand();
            String query = "INSERT INTO Company (name, created, modified) VALUES (@name, @created, @modified)";
            sql = new SqlCommand(query, connection);
            sql.Parameters.AddWithValue("@name", "Steve's Shoe Shop");
            sql.Parameters.AddWithValue("@created", DateTime.Now);
            sql.Parameters.AddWithValue("@modified", DateTime.Now);
            sql.ExecuteNonQuery();
        }
    }
}
