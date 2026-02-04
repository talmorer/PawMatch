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

            if (row == null || row.Length < 9)
                return null;

            p.PetID = int.Parse(row[0].ToString());
            p.PetName = row[1]?.ToString();
            p.PetAdress = row[2]?.ToString();
            p.PetPicture = row[3]?.ToString();
            p.PetType = int.Parse(row[4].ToString());
            p.PetBirthYear = int.Parse(row[5].ToString());
            p.UpdateUserID = int.Parse(row[6].ToString());
            p.IsActive = int.Parse(row[7].ToString());

            if (row[8] == null || row[8] == System.DBNull.Value)
                p.PetRaceID = 0;
            else
                p.PetRaceID = int.Parse(row[8].ToString());

            if (row.Length > 9)
            {
                p.TypeName = row.Length > 9 ? row[9]?.ToString() : null;
                p.RaceName = row.Length > 10 ? row[10]?.ToString() : null;

                p.UploaderFirstName = row.Length > 11 ? row[11]?.ToString() : null;
                p.UploaderLastName = row.Length > 12 ? row[12]?.ToString() : null;
                p.UploaderEmail = row.Length > 13 ? row[13]?.ToString() : null;
                p.UploaderPhone = row.Length > 14 ? row[14]?.ToString() : null;
            }

            return p;
        }

        public async Task<List<Pet>> SearchAsync(int typeID, int raceID, string text)
        {
            string sql = @"
SELECT 
    p.PetID, p.PetName, p.PetAdress, p.PetPicture, p.PetType, p.PetBirthYear, p.UpdateUserID, p.IsActive, p.PetRaceID,
    t.Description AS TypeName,
    r.Description AS RaceName,
    u.FirstName, u.LastName, u.Email, u.Phone
FROM pets p
INNER JOIN pet_type t ON t.TypeID = p.PetType
INNER JOIN pet_race r ON r.PetRaceID = p.PetRaceID
INNER JOIN users u ON u.UserID = p.UpdateUserID
WHERE p.IsActive = 1
";

            Dictionary<string, object> prms = new Dictionary<string, object>();

            if (typeID > 0)
            {
                sql += " AND p.PetType = @PetType";
                prms.Add("PetType", typeID);
            }

            if (raceID > 0)
            {
                sql += " AND p.PetRaceID = @PetRaceID";
                prms.Add("PetRaceID", raceID);
            }

            if (!string.IsNullOrEmpty(text))
            {
                sql += " AND (p.PetName LIKE @Txt OR p.PetAdress LIKE @Txt)";
                prms.Add("Txt", "%" + text + "%");
            }

            sql += " ORDER BY p.PetID DESC;";

            return (List<Pet>)await SelectAllAsync(sql, prms);
        }

        public async Task<Pet> InsertGetObjAsync(Pet p)
        {
            Dictionary<string, object> v = new Dictionary<string, object>()
            {
                { "PetName", p.PetName },
                { "PetAdress", p.PetAdress },
                { "PetPicture", p.PetPicture },
                { "PetType", p.PetType },
                { "PetBirthYear", p.PetBirthYear },
                { "UpdateUserID", p.UpdateUserID },
                { "IsActive", p.IsActive },
                { "PetRaceID", p.PetRaceID }
            };

            return await base.InsertGetObjAsync(v);
        }

        public async Task<int> UpdateAsync(Pet p)
        {
            Dictionary<string, object> v = new Dictionary<string, object>()
            {
                { "PetName", p.PetName },
                { "PetAdress", p.PetAdress },
                { "PetPicture", p.PetPicture },
                { "PetType", p.PetType },
                { "PetBirthYear", p.PetBirthYear },
                { "UpdateUserID", p.UpdateUserID },
                { "IsActive", p.IsActive },
                { "PetRaceID", p.PetRaceID }
            };

            Dictionary<string, object> filter = new Dictionary<string, object>()
            {
                { "PetID", p.PetID }
            };

            return await base.UpdateAsync(v, filter);
        }

        public async Task<List<Pet>> GetAllAsync()
        {
            return await SearchAsync(0, 0, "");
        }

        public async Task<List<Pet>> GetByUserAsync(int userId)
        {
            string sql = "SELECT * FROM pets WHERE UpdateUserID=@uid";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("uid", userId);
            return await SelectAllAsync(sql, p);
        }

        public async Task<int> UpdatePetAsync(Pet pet)
        {
            Dictionary<string, object> f = new Dictionary<string, object>();
            f.Add("PetName", pet.PetName);
            f.Add("PetAdress", pet.PetAdress);
            f.Add("PetType", pet.PetType);
            f.Add("PetRaceID", pet.PetRaceID);
            f.Add("PetBirthYear", pet.PetBirthYear);
            f.Add("PetPicture", pet.PetPicture);
            f.Add("IsActive", pet.IsActive);

            Dictionary<string, object> w = new Dictionary<string, object>();
            w.Add("PetID", pet.PetID);

            return await UpdateAsync(f, w);
        }

        public async Task<int> SetActiveAsync(int petId, int isActive)
        {
            Dictionary<string, object> f = new Dictionary<string, object>();
            f.Add("IsActive", isActive);

            Dictionary<string, object> w = new Dictionary<string, object>();
            w.Add("PetID", petId);

            return await UpdateAsync(f, w);
        }
    }
}
