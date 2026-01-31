using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DBL
{
    public class PetRaceDB : BaseDB<PetRace>
    {
        protected override string GetTableName() => "pet_race";
        protected override string GetPrimaryKeyName() => "PetRaceID";

        protected override Task<PetRace> CreateModelAsync(object[] row)
        {
            PetRace r = new PetRace();
            r.PetRaceID = (int)row[0];
            r.Description = (string)row[1];

            if (row[2] == null || row[2] == System.DBNull.Value)
                r.PetTypeID = null;
            else
                r.PetTypeID = (int)row[2];

            return Task.FromResult(r);
        }

        public async Task<List<PetRace>> GetByTypeAsync(int typeId)
        {
            string sql = "SELECT * FROM pet_race WHERE TypeID=@id";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("id", typeId);
            return await SelectAllAsync(sql, p);
        }
    }
}
