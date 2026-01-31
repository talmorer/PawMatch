using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DBL
{
    public class PetsTraitsDB : BaseDB<PetTrait>
    {
        protected override string GetTableName() => "pets_traits";
        protected override string GetPrimaryKeyName() => "PetID";

        protected override Task<PetTrait> CreateModelAsync(object[] row)
        {
            PetTrait pt = new PetTrait();
            pt.PetID = (int)row[0];
            pt.TraitID = (int)row[1];
            return Task.FromResult(pt);
        }

        public async Task<List<PetTrait>> GetAllAsync()
        {
            return await SelectAllAsync();
        }

        public async Task<int> DeleteByPetAsync(int petId)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("PetID", petId);
            return await DeleteAsync(p);
        }

        public async Task<int> InsertAsync(int petId, int traitId)
        {
            Dictionary<string, object> kv = new Dictionary<string, object>();
            kv.Add("PetID", petId);
            kv.Add("TraitID", traitId);
            return await InsertAsync(kv);
        }
    }
}
