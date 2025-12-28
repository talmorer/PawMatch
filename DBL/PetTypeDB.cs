using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DBL
{
    public class PetTypeDB : BaseDB<PetType>
    {
        protected override string GetTableName()
        {
            return "pet_type";
        }

        protected override string GetPrimaryKeyName()
        {
            return "TypeID";
        }

        protected override async Task<PetType> CreateModelAsync(object[] row)
        {
            PetType t = new PetType();

            t.TypeID = int.Parse(row[0].ToString());
            t.Description = row[1] == DBNull.Value ? null : row[1].ToString();

            return t;
        }

        public async Task<List<PetType>> GetAllAsync()
        {
            return (List<PetType>)await SelectAllAsync();
        }

        public async Task<PetType> InsertGetObjAsync(PetType t)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "Description", t.Description }
            };

            return await base.InsertGetObjAsync(values);
        }

        public async Task<int> InsertAsync(PetType t)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "Description", t.Description }
            };

            return await base.InsertAsync(values);
        }

        public async Task<int> UpdateAsync(PetType t)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "Description", t.Description }
            };

            Dictionary<string, object> filter = new Dictionary<string, object>()
            {
                { "TypeID", t.TypeID }
            };

            return await base.UpdateAsync(values, filter);
        }

        public async Task<int> DeleteAsync(int typeID)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>()
            {
                { "TypeID", typeID }
            };

            return await base.DeleteAsync(filter);
        }

        public async Task<PetType> SelectByPkAsync(int id)
        {
            var filter = new Dictionary<string, object>();
            filter.Add("TypeID", id);

            List<PetType> list = (List<PetType>)await SelectAllAsync(filter);
            if (list.Count == 1)
                return list[0];

            return null;
        }
    }
}














