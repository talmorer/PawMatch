using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DBL
{
    public class PetRaceDB : BaseDB<PetRace>
    {
        protected override string GetTableName()
        {
            return "pet_race";
        }

        protected override string GetPrimaryKeyName()
        {
            return "PetRaceID";
        }

        protected override async Task<PetRace> CreateModelAsync(object[] row)
        {
            PetRace r = new PetRace();

            r.PetRaceID = int.Parse(row[0].ToString());
            r.Description = row[1].ToString();
            r.PetTypeID = int.Parse(row[2].ToString());

            return r;
        }

        public async Task<List<PetRace>> GetAllAsync()
        {
            return (List<PetRace>)await SelectAllAsync();
        }

        public async Task<PetRace> InsertGetObjAsync(PetRace r)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "Description", r.Description },
                { "PetTypeID", r.PetTypeID }
            };

            return await base.InsertGetObjAsync(values);
        }

        public async Task<int> InsertAsync(PetRace r)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "Description", r.Description },
                { "PetTypeID", r.PetTypeID }
            };

            return await base.InsertAsync(values);
        }

        public async Task<int> UpdateAsync(PetRace r)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "Description", r.Description },
                { "PetTypeID", r.PetTypeID }
            };

            Dictionary<string, object> filter = new Dictionary<string, object>()
            {
                { "PetRaceID", r.PetRaceID }
            };

            return await base.UpdateAsync(values, filter);
        }

        public async Task<int> DeleteAsync(int petRaiceID)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>()
            {
                { "PetRaceID", petRaiceID }
            };

            return await base.DeleteAsync(filter);
        }

        public async Task<PetRace> SelectByPkAsync(int id)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>()
            {
                { "PetRaceID", id }
            };

            List<PetRace> list = (List<PetRace>)await SelectAllAsync(filter);

            if (list.Count == 1)
                return list[0];

            return null;
        }

        public async Task<List<PetRace>> GetByTypeAsync(int typeID)
        {
            string sql = "SELECT * FROM pet_race WHERE PetTypeID = @id";

            var p = new Dictionary<string, object>()
    {
        { "id", typeID }
    };

            return (List<PetRace>)await SelectAllAsync(sql, p);
        }

    }
}
