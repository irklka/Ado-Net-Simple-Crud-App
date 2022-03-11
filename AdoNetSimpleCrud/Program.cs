using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Ado.Net.Config;
using Ado.Net.Utility;

namespace Ado.Net
{
    public static class Program
    {
        static void Main(string[] args)
        {
            IDatabaseConfig databaseConfig = new DatabaseConfig();
            ISqlDataContext sqlDataContext = new SqlDataContext(databaseConfig, "DefaultConnection");
            CustomersProcessor customersProcessor = new CustomersProcessor (sqlDataContext);

            try
            {
                // Open connection
                Console.WriteLine("Connection opened");

                Console.WriteLine(customersProcessor.AddCustomers(DataSource.CustomersList));
                Console.WriteLine(customersProcessor.UpdateCustomer("First_Name", "Updated_Address"));
                Console.WriteLine(customersProcessor.DeleteCustomer("First_Name"));

                var res = customersProcessor.GetAllCustomers();

                foreach (var item in res)
                {
                    Console.WriteLine(item);
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Connection closed");
            }
        }
    }
}
