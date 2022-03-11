using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Ado.Net.Model;
using Ado.Net.Config;
using Ado.Net.Utility;

namespace Ado.Net
{
    /// <summary>
    /// Contains CRUD operations for interacting with provided database.
    /// </summary>
    public class CustomersProcessor
    {
        /// <summary>
        /// Connection string for using during interaction
        /// </summary>
        public ISqlDataContext _sqlDataContext { get; set; }

        public CustomersProcessor(ISqlDataContext sqlDataContext)
        {
            this._sqlDataContext = sqlDataContext;
        }

        public int AddCustomers(IEnumerable<Customer> customers)
        {
            if(customers == null) throw new ArgumentNullException(nameof(customers), "Value can not be a null.");

            string command = "insert into Customers values(@Name, @Address)";
            int valuesInserted = 0;

            var parameters = new List<SqlParameter>();

            foreach (var customer in customers)
            {
                parameters.Add(new SqlParameter("@Name", customer.Name));
                parameters.Add(new SqlParameter("@Address", customer.Address));

                valuesInserted += this._sqlDataContext.SaveData(command, parameters.ToArray());

                parameters = new List<SqlParameter>();
            }

            return valuesInserted;
        }

        public int AddCustomer(string name, string address)
        {
            if (name == null) throw new ArgumentNullException(nameof(name), "Value can not be a null.");
            if (address == null) throw new ArgumentNullException(nameof(address), "Value can not be a null.");

            string command = "insert into Customers values(@Name, @Address)";
            int valuesInserted = 0;

            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@Name", name));
            parameters.Add(new SqlParameter("@Address", address));

            valuesInserted += this._sqlDataContext.SaveData(command, parameters.ToArray());

            return valuesInserted;
        }

        public int UpdateCustomer(string name, string address)
        {
            if (name == null) throw new ArgumentNullException(nameof(name), "Value can not be a null.");
            if (address == null) throw new ArgumentNullException(nameof(address), "Value can not be a null.");

            string command = "update Customers set Address = @address where Name = @Name";

            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@Name", name));
            parameters.Add(new SqlParameter("@Address", address));

            return _sqlDataContext.UpdateData(command, parameters.ToArray());
        }

        public bool DeleteCustomer(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name), "Value can not be a null.");

            string command = "delete from Customers where Name = @Name";
            bool valuesDeleted = false;

            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@Name", name));
            valuesDeleted = _sqlDataContext.DeleteData(command, parameters.ToArray());
           

            return valuesDeleted;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            string command = "select * from Customers";
            var customers = new List<Customer>();

            var sqlReader = _sqlDataContext.RetriveData(command);

            while (sqlReader.Read())
            {
                customers.Add(new Customer {
                    Id = Convert.ToInt32(sqlReader["Id"]),
                    Name = sqlReader["Name"].ToString().Trim(),
                    Address = sqlReader["Address"].ToString().Trim()
                });

            }

            return customers;
        }
    }
}
