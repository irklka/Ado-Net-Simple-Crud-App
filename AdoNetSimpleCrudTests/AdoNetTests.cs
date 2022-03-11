using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ado.Net.Model;
using Ado.Net.Utility;
using Moq;
using NUnit.Framework;

#pragma warning disable CA1707

namespace Ado.Net.Tests
{
    /// <summary>
    /// Unit tests for CrudOperations.
    /// </summary>
    [TestFixture]
    public class AdoNetTests
    {
        /// <summary>
        /// AddCustomersTests throws ArgumentNullException if passed parameter is null.
        /// </summary>
        /// <param name="customers">parameter with null value. </param>
        [Test]
        public void AddCustomers_ThrowsExeption_ParameterIsNull()
        {
            Mock<ISqlDataContext> mockData = new Mock<ISqlDataContext>();

            CustomersProcessor cust = new CustomersProcessor(mockData.Object);

            mockData.Setup(x => x.SaveData(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);

            Assert.Throws<ArgumentNullException>(() => cust.AddCustomers(null));
        }

        /// <summary>
        /// AddCustomersTests return number of values inserted.
        /// </summary>
        /// <param name="customers">parameter with values to insert. </param>
        [Test]
        public void AddCustomers_ReturnValuesInserted()
        {
            var expected = GetSampleCustList();
            Mock<ISqlDataContext> mockData = new Mock<ISqlDataContext>();

            CustomersProcessor cust = new CustomersProcessor(mockData.Object);

            mockData.Setup(x => x.SaveData(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);

            var actual = cust.AddCustomers(expected);

            mockData.Verify(x => x.SaveData(It.IsAny<string>(), It.IsAny<SqlParameter[]>()), Times.Exactly(expected.Count));

            Assert.AreEqual(expected.Count, actual);
        }

        /// <summary>
        /// AddCustomer Throws ArgumentNullExeption if one of the parameters is null.
        /// </summary>
        /// <param name="name">value for name.</param>
        /// <param name="address">value for address.</param>
        [Test]
        [TestCase("Name1", null)]
        [TestCase(null, null)]
        [TestCase(null, "Address1")]
        public void AddCustomer_ThrowsExeption_ParameterIsNull(string name, string address)
        {
            Mock<ISqlDataContext> mockData = new Mock<ISqlDataContext>();

            CustomersProcessor cust = new CustomersProcessor(mockData.Object);

            mockData.Setup(x => x.SaveData(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);

            Assert.Throws<ArgumentNullException>(() => cust.AddCustomer(name, address));
        }

        /// <summary>
        /// AddCustomer returns true if value is inserted.
        /// </summary>
        /// <param name="name">value for name.</param>
        /// <param name="address">alue for address.</param>
        [Test]
        [TestCase("Name1", "Address1")]
        [TestCase("Name2", "Address2")]
        [TestCase("Name3", "Address3")]
        public void AddCustomer_ReturnsOne_WhenValueIsInserted(string name, string address)
        {
            var expected = 1;
            Mock<ISqlDataContext> mockData = new Mock<ISqlDataContext>();

            CustomersProcessor cust = new CustomersProcessor(mockData.Object);

            mockData.Setup(x => x.SaveData(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);

            var actual = cust.AddCustomer(name, address);

            mockData.Verify(x => x.SaveData(It.IsAny<string>(), It.IsAny<SqlParameter[]>()), Times.Once());

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// UpdateCustomer Throws ArgumentNullException for null value.
        /// </summary>
        /// <param name="name">value for name.</param>
        /// <param name="address">alue for address.</param>
        [Test]
        [TestCase("Name1", null)]
        [TestCase(null, null)]
        [TestCase(null, "Address1")]
        public void UpdateCustomer_ThrowsExeption_ParameterIsNull(string name, string address)
        {
            Mock<ISqlDataContext> mockData = new Mock<ISqlDataContext>();

            CustomersProcessor cust = new CustomersProcessor(mockData.Object);

            mockData.Setup(x => x.UpdateData(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);

            Assert.Throws<ArgumentNullException>(() => cust.UpdateCustomer(name, address));
        }

        /// <summary>
        /// UpdateCustomer returns true if value is updated.
        /// </summary>
        /// <param name="name">value for name.</param>
        /// <param name="address">alue for address.</param>
        /// <returns>Expected Result.</returns>
        [Test]
        [TestCase("Name1", "Address1", ExpectedResult = 1)]
        [TestCase("Name2", "Address2", ExpectedResult = 1)]
        [TestCase("Name3", "Address3", ExpectedResult = 1)]
        public int UpdateCustomer_ReturnsTrue_WhenValuesUpdated(string name, string address)
        {
            var actual = 0;

            Mock<ISqlDataContext> mockData = new Mock<ISqlDataContext>();

            CustomersProcessor cust = new CustomersProcessor(mockData.Object);

            mockData.Setup(x => x.UpdateData(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);

            actual = cust.UpdateCustomer(name, address);

            mockData.Verify(x => x.UpdateData(It.IsAny<string>(), It.IsAny<SqlParameter[]>()), Times.Once());

            return actual;
        }

        /// <summary>
        /// DeleteCustomer Throws ArgumentNullException for null value.
        /// </summary>
        /// <param name="name">value for name.</param>
        [Test]
        public void DeleteCustomer_ThrowsExeption_ParameterIsNull()
        {
            Mock<ISqlDataContext> mockData = new Mock<ISqlDataContext>();

            CustomersProcessor cust = new CustomersProcessor(mockData.Object);

            mockData.Setup(x => x.DeleteData(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(true);

            Assert.Throws<ArgumentNullException>(() => cust.DeleteCustomer(null));
        }

        /// <summary>
        /// DeleteCustomer returns true if value is deleted.
        /// </summary>
        /// <param name="name">value for name.</param>
        /// <returns>Expected Result.</returns>
        [Test]
        [TestCase("Name1", ExpectedResult = true)]
        [TestCase("Name2", ExpectedResult = true)]
        [TestCase("Name3", ExpectedResult = true)]
        public bool DeleteCustomer_ReturnsTrue_WhenValuesDeleted(string name)
        {
            var actual = false;

            Mock<ISqlDataContext> mockData = new Mock<ISqlDataContext>();

            CustomersProcessor cust = new CustomersProcessor(mockData.Object);

            mockData.Setup(x => x.DeleteData(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(true);

            actual = cust.DeleteCustomer(name);

            mockData.Verify(x => x.DeleteData(It.IsAny<string>(), It.IsAny<SqlParameter[]>()), Times.Exactly(1));

            return actual;
        }

        /// <summary>
        /// GetAllCustomers calls Database for retriving data exactly once.
        /// </summary>
        [Test]
        public void GetAllCustomers_CallsDatabaseForRetrival()
        {
            Mock<ISqlDataContext> mockData = new Mock<ISqlDataContext>();

            CustomersProcessor cust = new CustomersProcessor(mockData.Object);
            var dr = new DataTableReader(new DataTable());

            mockData.Setup(x => x.RetriveData(It.IsAny<string>())).Returns(dr);
            
            cust.GetAllCustomers();

            mockData.Verify(x => x.RetriveData(It.IsAny<string>()), Times.Exactly(1));
            dr.Close();
        }

        private static List<Customer> GetSampleCustList()
        {
            return new List<Customer>()
            {
                new Customer() { Name = "Name1", Address = "Address1" },
                new Customer() { Name = "Name2", Address = "Address2" },
                new Customer() { Name = "Name3", Address = "Address3" },
            };
        }
    }
}
