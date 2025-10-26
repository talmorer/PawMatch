using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Models;

namespace DBL
{
    public class CustomerDB : BaseDB<Customer>
    {
        protected override string GetTableName()
        {
            return "Customers";
        }

        protected override string GetPrimaryKeyName()
        {
            return "CustomerID";
        }

        protected override async Task<Customer> CreateModelAsync(object[] row)
        {
            Customer c = new Customer();
            c.UserID = int.Parse(row[0].ToString());
            c.IsAdmin = int.Parse(row[1].ToString());
            c.FirstName = row[2].ToString();
            c.LastName = row[3].ToString();
            c.Email = row[4].ToString();
            c.Phone = int.Parse(row[5].ToString());
            c.Email = row[4].ToString();
            c.IsAdmin = int.Parse(row[1].ToString());
            return c;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return ((List<Customer>)await SelectAllAsync());
        }

        public async Task<Customer> InsertGetObjAsync(Customer customer, string password)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { " First Name", customer.FirstName },
                { "Last Name",customer.LastName },      
                { "Email", customer.Email },
                { "CustomerPassword", password }
            };
            return (Customer)await base.InsertGetObjAsync(fillValues);
        }

        public async Task<int> UpdateAsync(Customer customer)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            fillValues.Add(" First Name", customer.FirstName);
            fillValues.Add("Last Name", customer.LastName);
            fillValues.Add("Email", customer.Email);
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<int> DeleteAsync(Customer customer)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>
            {
                { "CustomerID", customer.Id.ToString() }
            };
            return await base.DeleteAsync(filterValues);
        }

        public async Task<Customer> SelectByPkAsync(int id)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("CustomerID", id);
            List<Customer> list = (List<Customer>)await SelectAllAsync(p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

        public async Task<Customer> GetCustomerByOrderIDAsync(int orderID)
        {
            string sql = @$"Select mystore.customers.*
                           From mystore.customers Inner Join mystore.orders 
                           On mystore.orders.CustomerID = mystore.customers.CustomerID";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("mystore.orders.OrderID", orderID.ToString());
            List<Customer> list = (List<Customer>)await SelectAllAsync(sql, p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

        public async Task<List<(string, string)>> GetNameAndEmail4NonAdminsAsync()
        {
            List<(string, string)> returnList = new List<(string, string)>();
            string sql = "select * from Customers";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("IsAdmin", "0");
            List<Customer> list = (List<Customer>)await SelectAllAsync(sql, p);
            foreach (Customer item in list)
            {
                returnList.Add((item.Name, item.Email));
            }
            return returnList;
        }
    }
}