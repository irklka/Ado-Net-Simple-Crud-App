using System;
using System.Collections.Generic;
using System.Text;
using Ado.Net.Model;

namespace Ado.Net
{
    public static class DataSource
    {
        public static IEnumerable<Customer> CustomersList 
        {
            get => Customers;
        }

        private static readonly IEnumerable<Customer> Customers = new List<Customer>()
        {
            new Customer() { Name = "First_Name", Address = "First_Address"},
            new Customer() { Name = "Second_Name", Address = "Second_Address"},
            new Customer() { Name = "Third_Name", Address = "Third_Address"}
        };
    }
}
