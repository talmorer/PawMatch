using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DBL
{
    public class PetsDB : BaseDB<Pet>
    {
        protected override string GetTableName()
        {
            return "pets";
        }

        protected override string GetPrimaryKeyName()
        {
            return "PetID";
        }

        protected override async Task<Pet> CreateModelAsync(object[] row)
        {
            Pet p = new Pet();

            p.PetID = int.Parse(row[0].ToString());
            p.PetName = row[1].ToString();
            p.PetAdress = row[2].ToString();
            p.PetPicture = row[3].ToString();
            p.PetType = int.Parse(row[4].ToString());
            p.PetBirthYear = int.Parse(row[5].ToString());
            p.UpdateUserID = int.Parse(row[6].ToString());
            p.IsActive = int.Parse(row[7].ToString());

            return p;
        }

        // Get all pets
        public async Task<List<Pet>> GetAllAsync()
        {
            return (List<Pet>)await SelectAllAsync();
        }

        // Get only active pets
        public async Task<List<Pet>> GetActiveAsync()
        {
            var filter = new Dictionary<string, object>();
            filter.Add("IsActive", 1);

            return (List<Pet>)await SelectAllAsync(filter);
        }

        // Insert pet and get object back
        public async Task<Pet> InsertGetObjAsync(Pet pet)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "PetName", pet.PetName },
                { "PetAdress", pet.PetAdress },
                { "PetPicture", pet.PetPicture },
                { "PetType", pet.PetType },
                { "PetBi rthYear", pet.PetBirthYear },
                { "UpdateUserID", pet.UpdateUserID },
                { "IsActive", pet.IsActive }
            };

            return await base.InsertGetObjAsync(values);
        }

        // Insert pet (no return)
        public async Task<int> InsertAsync(Pet pet)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "PetName", pet.PetName },
                { "PetAdress", pet.PetAdress },
                { "PetPicture", pet.PetPicture },
                { "PetType", pet.PetType },
                { "PetBirthYear", pet.PetBirthYear },
                { "UpdateUserID", pet.UpdateUserID },
                { "IsActive", pet.IsActive }
            };

            return await base.InsertAsync(values);
        }

        // Update pet
        public async Task<int> UpdateAsync(Pet pet)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "PetName", pet.PetName },
                { "PetAdress", pet.PetAdress },
                { "PetPicture", pet.PetPicture },
                { "PetType", pet.PetType },
                { "PetBirthYear", pet.PetBirthYear },
                { "UpdateUserID", pet.UpdateUserID },
                { "IsActive", pet.IsActive }
            };

            Dictionary<string, object> filter = new Dictionary<string, object>()
            {
                { "PetID", pet.PetID }
            };

            return await base.UpdateAsync(values, filter);
        }

        // Delete pet
        public async Task<int> DeleteAsync(int petID)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>()
            {
                { "PetID", petID }
            };

            return await base.DeleteAsync(filter);
        }

        // Select pet by ID
        public async Task<Pet> SelectByPkAsync(int id)
        {
            var filter = new Dictionary<string, object>();
            filter.Add("PetID", id);

            List<Pet> list = (List<Pet>)await SelectAllAsync(filter);
            if (list.Count == 1)
                return list[0];

            return null;
        }
    }
}
