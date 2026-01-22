using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DBL
{
    public class PetTraitsDB : BaseDB<Trait>
    {
        protected override string GetTableName() => "pet_traits";
        protected override string GetPrimaryKeyName() => "TraitID";

        protected override Task<Trait> CreateModelAsync(object[] row)
        {
            Trait t = new Trait();
            t.TraitID = (int)row[0];
            t.TraitName = (string)row[1];
            return Task.FromResult(t);
        }

        public async Task<List<Trait>> GetAllAsync()
        {
            return await SelectAllAsync();
        }

        public async Task<Trait> InsertGetObjAsync(Trait t)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("TraitName", t.TraitName);
            return await InsertGetObjAsync(d);
        }
    }
}
