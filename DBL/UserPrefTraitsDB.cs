using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DBL
{
    public class UserPrefsTraitsDB : BaseDB<UserPrefTrait>
    {
        protected override string GetTableName() => "user_prefs_traits";
        protected override string GetPrimaryKeyName() => "";

        protected override Task<UserPrefTrait> CreateModelAsync(object[] row)
        {
            UserPrefTrait x = new UserPrefTrait();
            x.UserID = (int)row[0];
            x.TraitID = (int)row[1];
            return Task.FromResult(x);
        }

        public async Task<List<UserPrefTrait>> GetByUserAsync(int userId)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("UserID", userId);
            return await SelectAllAsync(p);
        }

        public async Task<int> DeleteByUserAsync(int userId)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("UserID", userId);
            return await DeleteAsync(p);
        }
        public async Task<int> InsertAsync(int userId, int traitId)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("UserID", userId);
            d.Add("TraitID", traitId);
            return await InsertAsync(d);
        }
    }
}