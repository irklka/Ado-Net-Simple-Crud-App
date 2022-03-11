using System;
using System.Collections.Generic;
using System.Text;

namespace Ado.Net.Model
{
    /// <summary>
    /// Customer model with proper formating for ToString();
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"Id: {Id,-5} Name: {Name,-15} Address:{Address}";
        }
    }
}
