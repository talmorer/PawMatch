using DBL;
using Models;

namespace ConsoleUnitTesting
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            CustomerDB db = new CustomerDB();

            Customer customer = new Customer
            {                
                FirstName = "ssss",
                LastName = "gal",
                Email = "gal@gmail.com",
                Phone = "0523581648",
                Password = "1234"
            };

            customer = await db.InsertGetObjAsync(customer);
            if (customer == null)
            {
                Console.WriteLine("failed");
                return;
            }
        }
    }
}
