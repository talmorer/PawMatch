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
            //CustomerDB db = new CustomerDB();
            //Console.WriteLine("Testing SelectByEmailAndPasswordAsync...\n");
            //string email = "omer.peretz@example.com";   
            //string password = "omer258";         
            //Customer found = await db.SelectByEmailAndPasswordAsync(email, password);
            //if (found != null)
            //{
            //    Console.WriteLine($"Found user:");               
            //    Console.WriteLine($"ID: {found.UserID}");
            //    Console.WriteLine($"Name: {found.FirstName} {found.LastName}");
            //    Console.WriteLine($"Email: {found.Email}");
            //    Console.WriteLine($"Phone: {found.Phone}");
            //    Console.WriteLine($"Admin: {found.IsAdmin}");
            //}
            //else
            //{
            //    Console.WriteLine("User not found");
            //}


            // ----- PetTypeDB tests -----
            PetTypeDB typeDb = new PetTypeDB();

            // InsertGetObj (PetType)
            PetType t = new PetType
            {
                Description = "TestType_" + Guid.NewGuid().ToString("N").Substring(0, 8)
            };

            t = await typeDb.InsertGetObjAsync(t);
            if (t == null)
            {
                Console.WriteLine("PetType InsertGetObjAsync failed");
                return;
            }
            Console.WriteLine($"PetType inserted: TypeID={t.TypeID}, Description={t.Description}");

            // SelectByPk (PetType)
            PetType t2 = await typeDb.SelectByPkAsync(t.TypeID);
            if (t2 == null)
            {
                Console.WriteLine("PetType SelectByPkAsync failed");
                return;
            }
            Console.WriteLine($"PetType selected: TypeID={t2.TypeID}, Description={t2.Description}");

            // Update (PetType)
            t2.Description = t2.Description + "_Updated";
            int upType = await typeDb.UpdateAsync(t2);
            if (upType <= 0)
            {
                Console.WriteLine("PetType UpdateAsync failed");
                return;
            }
            Console.WriteLine("PetType updated");

            // GetAll (PetType)
            var allTypes = await typeDb.GetAllAsync();
            Console.WriteLine($"PetType GetAllAsync count: {allTypes.Count}");

            // ----- PetRaiceDB tests -----
            PetRaiceDB raiceDb = new PetRaiceDB();

            // InsertGetObj (PetRaice)
            PetRaice r = new PetRaice
            {
                Description = "TestRaice_" + Guid.NewGuid().ToString("N").Substring(0, 8),
                PetTypeID = t.TypeID
            };

            r = await raiceDb.InsertGetObjAsync(r);
            if (r == null)
            {
                Console.WriteLine("PetRaice InsertGetObjAsync failed");
                return;
            }
            Console.WriteLine($"PetRaice inserted: PetRaiceID={r.PetRaiceID}, Description={r.Description}, PetTypeID={r.PetTypeID}");

            // SelectByPk (PetRaice)
            PetRaice r2 = await raiceDb.SelectByPkAsync(r.PetRaiceID);
            if (r2 == null)
            {
                Console.WriteLine("PetRaice SelectByPkAsync failed");
                return;
            }
            Console.WriteLine($"PetRaice selected: PetRaiceID={r2.PetRaiceID}, Description={r2.Description}, PetTypeID={r2.PetTypeID}");

            // Update (PetRaice)
            r2.Description = r2.Description + "_Updated";
            int upRaice = await raiceDb.UpdateAsync(r2);
            if (upRaice <= 0)
            {
                Console.WriteLine("PetRaice UpdateAsync failed");
                return;
            }
            Console.WriteLine("PetRaice updated");

            // GetAll (PetRaice)
            var allRaices = await raiceDb.GetAllAsync();
            Console.WriteLine($"PetRaice GetAllAsync count: {allRaices.Count}");

            // ----- Cleanup -----
            int delRaice = await raiceDb.DeleteAsync(r2.PetRaiceID);
            Console.WriteLine(delRaice > 0 ? "PetRaice deleted" : "PetRaice delete failed");

            int delType = await typeDb.DeleteAsync(t2.TypeID);
            Console.WriteLine(delType > 0 ? "PetType deleted" : "PetType delete failed");

            Console.WriteLine("Done");
        }
    }
}



        