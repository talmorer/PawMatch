using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DBL
{
    public class PetRaiceDB : BaseDB<PetRaice>
    {
        protected override string GetTableName()
        {
            return "pet_raice";
        }

        protected override string GetPrimaryKeyName()
        {
            return "PetRaiceID";
        }

        protected override async Task<PetRaice> CreateModelAsync(object[] row)
        {
            PetRaice r = new PetRaice();

            r.PetRaiceID = int.Parse(row[0].ToString());
            r.Description = row[1] == DBNull.Value ? null : row[1].ToString();
            r.PetTypeID = row[2] == DBNull.Value ? (int?)null : int.Parse(row[2].ToString());

            return r;
        }

        public async Task<List<PetRaice>> GetAllAsync()
        {
            return (List<PetRaice>)await SelectAllAsync();
        }

        public async Task<PetRaice> InsertGetObjAsync(PetRaice r)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "Description", r.Description },
                { "PetTypeID", r.PetTypeID }
            };

            return await base.InsertGetObjAsync(values);
        }

        public async Task<int> InsertAsync(PetRaice r)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "Description", r.Description },
                { "PetTypeID", r.PetTypeID }
            };

            return await base.InsertAsync(values);
        }

        public async Task<int> UpdateAsync(PetRaice r)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "Description", r.Description },
                { "PetTypeID", r.PetTypeID }
            };

            Dictionary<string, object> filter = new Dictionary<string, object>()
            {
                { "PetRaiceID", r.PetRaiceID }
            };

            return await base.UpdateAsync(values, filter);
        }

        public async Task<int> DeleteAsync(int petRaiceID)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>()
            {
                { "PetRaiceID", petRaiceID }
            };

            return await base.DeleteAsync(filter);
        }

        public async Task<PetRaice> SelectByPkAsync(int id)
        {
            var filter = new Dictionary<string, object>();
            filter.Add("PetRaiceID", id);

            List<PetRaice> list = (List<PetRaice>)await SelectAllAsync(filter);
            if (list.Count == 1)
                return list[0];

            return null;
        }
    }
}
