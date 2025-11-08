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
            return "users";
        }

        protected override string GetPrimaryKeyName()
        {
            return "UserID";
        }

        protected override async Task<Customer> CreateModelAsync(object[] row)
        {
            Customer c = new Customer();
            c.UserID = int.Parse(row[0].ToString());
            c.IsAdmin = bool.Parse(row[1].ToString());
            c.FirstName = row[2].ToString();
            c.LastName = row[3].ToString();
            c.Email = row[4].ToString();
            c.Phone = row[5].ToString();
            c.Password = row[6].ToString();

            return c;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return ((List<Customer>)await SelectAllAsync());
        }

        public async Task<Customer> InsertGetObjAsync(Customer customer)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                 { "FirstName", customer.FirstName },
                 { "LastName", customer.LastName },
                 { "Email", customer.Email },
                 { "Phone", customer.Phone },
                 { "Password", customer.Password }
            };   
            return (Customer)await base.InsertGetObjAsync(fillValues);
        }

        public async Task<int> UpdateAsync(Customer customer)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            fillValues.Add("FirstName", customer.FirstName);
            fillValues.Add("LastName", customer.LastName);
            fillValues.Add("Phone", customer.Phone);
            fillValues.Add("Password", customer.Password);
            filterValues.Add("UserID", customer.UserID);
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<int> DeleteAsync(Customer customer)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>
            {
                { "UserID", customer.UserID.ToString() }
            };
            return await base.DeleteAsync(filterValues);
        }

        public async Task<Customer> SelectByPkAsync(int id)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("UserID", id);
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
        public async Task<Customer> SelectByEmailAndPasswordAsync(string email, string password)
        {
            string sql = "SELECT * FROM users WHERE Email=@Email AND Password=@Password";
            var p = new Dictionary<string, object>
    {
        { "Email", email },
        { "Password", password }
    };

            var list = (List<Customer>)await SelectAllAsync(sql, p);
            if (list.Count == 1)
                return list[0];
            return null;
        }


        //public async Task<List<(string, string)>> GetNameAndEmail4NonAdminsAsync()
        //{
        //    List<(string, string)> returnList = new List<(string, string)>();
        //    string sql = "select * from Customers";
        //    Dictionary<string, object> p = new Dictionary<string, object>();
        //    p.Add("IsAdmin", "0");
        //    List<Customer> list = (List<Customer>)await SelectAllAsync(sql, p);
        //    foreach (Customer item in list)
        //    {
        //        returnList.Add((item.Name, item.Email));
        //    }
        //    return returnList;
        //}
    }
}