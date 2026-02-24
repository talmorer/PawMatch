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

            r.PetRaceID = int.Parse(row[0].ToString());
            r.Description = row[1].ToString();

            if (row[2] == null || row[2] == System.DBNull.Value)
                r.PetTypeID = null;
            else
                r.PetTypeID = int.Parse(row[2].ToString());

            return Task.FromResult(r);
        }

        public async Task<List<PetRace>> GetByTypeAsync(int typeId)
        {
            string sql = "SELECT * FROM pet_race WHERE PetTypeID=@id";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("id", typeId);
            return await SelectAllAsync(sql, p);
        }
    }
}