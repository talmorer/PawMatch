using DBL;
using Models;

namespace ConsoleUnitTesting
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Register
            //CustomerDB db = new CustomerDB();
            //Customer customer = new Customer
            //{                
            //    FirstName = "galgalz",
            //    LastName = "gal",
            //    Email = "gal@gmail.com",
            //    Phone = "0523581648",
            //    Password = "1234"
            //}
            //customer = await db.InsertGetObjAsync(customer);
            //if (customer == null)
            //{
            //    Console.WriteLine("failed");
            //    return;
            //}

            //Login
            //CustomerDB db = new CustomerDB();
            //Console.Write("Enter email: ");
            //string email = Console.ReadLine();
            //Console.Write("Enter password: ");
            //string password = Console.ReadLine();
            //Customer customer = await db.SelectByEmailAndPasswordAsync(email, password);
            //if (customer != null)
            //{
            //    await Console.Out.WriteLineAsync($" Login success! Welcome {customer.FirstName} {customer.LastName}");
            //}
            //else
            //{
            //    await Console.Out.WriteLineAsync(" Login failed. Wrong email or password.");
            //}

            ////Update & Select by Pk
            //CustomerDB db = new CustomerDB();
            //Customer customer = await db.SelectByPkAsync(2);
            //if (customer == null)
            //{
            //    Console.WriteLine(" No user found with this ID.");
            //    return;
            //}
            //Console.WriteLine($"Before update: {customer.FirstName} {customer.LastName}");
            //customer.FirstName = "UpdatedFirst";
            //customer.LastName = "UpdatedLast";
            //customer.Email = "updated@gmail.com";
            //customer.Phone = "0500000000";
            //customer.Password = "newpass";
            //int rows = await db.UpdateAsync(customer);
            //if (rows > 0)
            //{
            //    Console.WriteLine("Update successful!");
            //    Customer updated = await db.SelectByPkAsync(customer.UserID);
            //    Console.WriteLine($"After update: {updated.FirstName} {updated.LastName}, Email: {updated.Email}");
            //}
            //else
            //{
            //    Console.WriteLine("Update failed (no rows affected).");
            //}

            //Select All
            //CustomerDB db = new CustomerDB();
            //var allUsers = await db.GetAllAsync();
            //Console.WriteLine($"Total users found: {allUsers.Count}");
            //foreach (var u in allUsers)
            //{
            //    Console.WriteLine($"ID: {u.UserID}, Name: {u.FirstName} {u.LastName}, Email: {u.Email}");
            //}

            //Delete
            //CustomerDB db = new CustomerDB();
            //var allUsers = await db.GetAllAsync();
            //if (allUsers.Count > 1)
            //{
            //    var userToDelete = allUsers[1];
            //    Console.WriteLine($"Deleting user ID {userToDelete.UserID} ({userToDelete.Email})...");
            //    int rows = await db.DeleteAsync(userToDelete);
            //    if (rows > 0)
            //    {
            //        Console.WriteLine("User deleted successfully!");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Delete failed.");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Not enough users to test delete.");
            //}

            //Select by EMAIL and PASSWORD
            CustomerDB db = new CustomerDB();
            Console.WriteLine("Testing SelectByEmailAndPasswordAsync...\n");
            string email = "omer.peretz@example.com";   
            string password = "omer258";         
            Customer found = await db.SelectByEmailAndPasswordAsync(email, password);
            if (found != null)
            {
                Console.WriteLine($"Found user:");               
                Console.WriteLine($"ID: {found.UserID}");
                Console.WriteLine($"Name: {found.FirstName} {found.LastName}");
                Console.WriteLine($"Email: {found.Email}");
                Console.WriteLine($"Phone: {found.Phone}");
                Console.WriteLine($"Admin: {found.IsAdmin}");
            }
            else
            {
                Console.WriteLine("User not found");
            }
        }
    }
}



        