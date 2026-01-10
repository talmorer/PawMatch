//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using DBL;
//using Models;

//namespace ConsoleUnitTesting
//{
//    internal class Program
//    {
//        static async Task Main(string[] args)
//        {
//            Console.WriteLine("=== Console DB Tests Started ===");

//            CustomerDB udb = new CustomerDB();
//            UserPrefsDB pdb = new UserPrefsDB();
//            PetTypeDB tdb = new PetTypeDB();
//            PetRaceDB rdb = new PetRaceDB();
//            PetsDB_Test petDb = new PetsDB_Test();

//            Customer u = null;
//            UserPrefs pr = null;
//            PetType t = null;
//            PetRace r = null;
//            Pet p = null;

//            try
//            {
//                // -------------------------
//                // 1) CustomerDB - InsertGetObj + SelectByPk + SelectByEmailAndPassword + Update
//                // -------------------------
//                Console.WriteLine("\n[1] Testing CustomerDB...");

//                string e = "test_" + Guid.NewGuid().ToString("N").Substring(0, 8) + "@mail.com";

//                u = new Customer
//                {
//                    FirstName = "Test",
//                    LastName = "User",
//                    Email = e,
//                    Phone = "0500000000",
//                    Password = "1234"
//                };

//                u = await udb.InsertGetObjAsync(u);
//                if (u == null)
//                {
//                    Console.WriteLine("Customer InsertGetObjAsync failed");
//                    return;
//                }
//                Console.WriteLine("Customer inserted: UserID=" + u.UserID + ", Email=" + u.Email);

//                Customer u2 = await udb.SelectByPkAsync(u.UserID);
//                if (u2 == null)
//                {
//                    Console.WriteLine("Customer SelectByPkAsync failed");
//                    return;
//                }
//                Console.WriteLine("Customer selected by PK: UserID=" + u2.UserID);

//                Customer u3 = await udb.SelectByEmailAndPasswordAsync(u.Email, u.Password);
//                if (u3 == null)
//                {
//                    Console.WriteLine("Customer SelectByEmailAndPasswordAsync failed");
//                    return;
//                }
//                Console.WriteLine("Customer login OK: UserID=" + u3.UserID);

//                u2.FirstName = "Test_Updated";
//                int ur = await udb.UpdateAsync(u2);
//                if (ur <= 0)
//                {
//                    Console.WriteLine("Customer UpdateAsync failed");
//                    return;
//                }
//                Console.WriteLine("Customer updated");

//                // -------------------------
//                // 2) UserPrefsDB - GetPrefs + SavePrefs + GetPrefs again
//                // -------------------------
//                Console.WriteLine("\n[2] Testing UserPrefsDB...");

//                pr = await pdb.GetPrefsAsync(u.UserID);

//                if (pr == null)
//                {
//                    pr = new UserPrefs
//                    {
//                        UserID = u.UserID,
//                        CanAdopt = 1,
//                        CanUpload = 1
//                    };

//                    await pdb.SavePrefsAsync(pr);
//                    Console.WriteLine("UserPrefs created");
//                }
//                else
//                {
//                    pr.CanAdopt = pr.CanAdopt == 1 ? 0 : 1;
//                    pr.CanUpload = pr.CanUpload == 1 ? 0 : 1;
//                    await pdb.SavePrefsAsync(pr);
//                    Console.WriteLine("UserPrefs updated");
//                }

//                UserPrefs pr2 = await pdb.GetPrefsAsync(u.UserID);
//                if (pr2 == null)
//                {
//                    Console.WriteLine("UserPrefs GetPrefsAsync failed after save");
//                    return;
//                }
//                Console.WriteLine("UserPrefs loaded: UserID=" + pr2.UserID + ", CanAdopt=" + pr2.CanAdopt + ", CanUpload=" + pr2.CanUpload);

//                // -------------------------
//                // 3) PetTypeDB - InsertGetObj + SelectByPk + Update + GetAll
//                // -------------------------
//                Console.WriteLine("\n[3] Testing PetTypeDB...");

//                t = new PetType
//                {
//                    Description = "TestType_" + Guid.NewGuid().ToString("N").Substring(0, 8)
//                };

//                t = await tdb.InsertGetObjAsync(t);
//                if (t == null)
//                {
//                    Console.WriteLine("PetType InsertGetObjAsync failed");
//                    return;
//                }
//                Console.WriteLine("PetType inserted: TypeID=" + t.TypeID + ", Desc=" + t.Description);

//                PetType t2 = await tdb.SelectByPkAsync(t.TypeID);
//                if (t2 == null)
//                {
//                    Console.WriteLine("PetType SelectByPkAsync failed");
//                    return;
//                }
//                Console.WriteLine("PetType selected: TypeID=" + t2.TypeID);

//                t2.Description = t2.Description + "_Updated";
//                int tr = await tdb.UpdateAsync(t2);
//                if (tr <= 0)
//                {
//                    Console.WriteLine("PetType UpdateAsync failed");
//                    return;
//                }
//                Console.WriteLine("PetType updated");

//                List<PetType> tl = await tdb.GetAllAsync();
//                Console.WriteLine("PetType GetAllAsync count: " + tl.Count);

//                // -------------------------
//                // 4) PetRaceDB - InsertGetObj + SelectByPk + Update + GetByType + GetAll
//                // -------------------------
//                Console.WriteLine("\n[4] Testing PetRaceDB...");

//                r = new PetRace
//                {
//                    Description = "TestRace_" + Guid.NewGuid().ToString("N").Substring(0, 8),
//                    PetTypeID = t.TypeID
//                };

//                r = await rdb.InsertGetObjAsync(r);
//                if (r == null)
//                {
//                    Console.WriteLine("PetRace InsertGetObjAsync failed");
//                    return;
//                }
//                Console.WriteLine("PetRace inserted: PetRaceID=" + r.PetRaceID + ", Desc=" + r.Description + ", PetTypeID=" + r.PetTypeID);

//                PetRace r2 = await rdb.SelectByPkAsync(r.PetRaceID);
//                if (r2 == null)
//                {
//                    Console.WriteLine("PetRace SelectByPkAsync failed");
//                    return;
//                }
//                Console.WriteLine("PetRace selected: PetRaceID=" + r2.PetRaceID);

//                r2.Description = r2.Description + "_Updated";
//                int rr = await rdb.UpdateAsync(r2);
//                if (rr <= 0)
//                {
//                    Console.WriteLine("PetRace UpdateAsync failed");
//                    return;
//                }
//                Console.WriteLine("PetRace updated");

//                List<PetRace> rl1 = await rdb.GetByTypeAsync(t.TypeID);
//                Console.WriteLine("PetRace GetByTypeAsync count: " + rl1.Count);

//                List<PetRace> rl2 = await rdb.GetAllAsync();
//                Console.WriteLine("PetRace GetAllAsync count: " + rl2.Count);

//                // -------------------------
//                // 5) PetsDB - InsertGetObj + Update + GetAll + Delete (via test wrapper)
//                // -------------------------
//                Console.WriteLine("\n[5] Testing PetsDB...");

//                p = new Pet
//                {
//                    PetName = "TestPet_" + Guid.NewGuid().ToString("N").Substring(0, 6),
//                    PetAdress = "TestCity",
//                    PetPicture = "test.jpg",
//                    PetType = t.TypeID,
//                    PetRaceID = r.PetRaceID,
//                    PetBirthYear = 2020,
//                    UpdateUserID = u.UserID,
//                    IsActive = 1
//                };

//                p = await petDb.InsertGetObjAsync(p);
//                if (p == null)
//                {
//                    Console.WriteLine("Pets InsertGetObjAsync failed");
//                    return;
//                }
//                Console.WriteLine("Pet inserted: PetID=" + p.PetID + ", Name=" + p.PetName);

//                p.PetName = p.PetName + "_Updated";
//                p.PetAdress = "TestCity_Updated";
//                p.IsActive = 1;

//                int prw = await petDb.UpdateAsync(p);
//                if (prw <= 0)
//                {
//                    Console.WriteLine("Pets UpdateAsync failed");
//                    return;
//                }
//                Console.WriteLine("Pet updated");

//                List<Pet> pl = await petDb.GetAllAsync();
//                Console.WriteLine("Pets GetAllAsync count: " + pl.Count);

//                int pd = await petDb.DeleteByIdAsync(p.PetID);
//                if (pd <= 0)
//                {
//                    Console.WriteLine("Pets delete failed (FK or missing permissions)");
//                    Console.WriteLine("Stopping cleanup to avoid breaking FK chain.");
//                    Console.WriteLine("Done");
//                    return;
//                }
//                Console.WriteLine("Pet deleted");

//                // -------------------------
//                // Cleanup: delete race then type (now that pet is deleted)
//                // -------------------------
//                int dr = await rdb.DeleteAsync(r.PetRaceID);
//                Console.WriteLine(dr > 0 ? "PetRace deleted" : "PetRace delete failed");

//                int dt = await tdb.DeleteAsync(t.TypeID);
//                Console.WriteLine(dt > 0 ? "PetType deleted" : "PetType delete failed");

//                // Optional: delete user (may fail if FK exists from other tables)
//                int du = await udb.DeleteAsync(u);
//                Console.WriteLine(du > 0 ? "Customer deleted" : "Customer delete failed (FK may exist)");

//                Console.WriteLine("\n=== All Tests Finished Successfully ===");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("\nTest crashed:");
//                Console.WriteLine(ex.Message);
//            }
//        }
//    }

//    public class PetsDB_Test : PetsDB
//    {
//        public async Task<int> DeleteByIdAsync(int id)
//        {
//            Dictionary<string, object> f = new Dictionary<string, object>();
//            f.Add("PetID", id);
//            return await base.DeleteAsync(f);
//        }
//    }
//}
