using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DBL
{
    public class UserPrefsDB : BaseDB<UserPrefs>
    {
        protected override string GetTableName()
        {
            return "user_prefs";
        }

        protected override string GetPrimaryKeyName()
        {
            return "UserID";
        }

        protected override async Task<UserPrefs> CreateModelAsync(object[] row)
        {
            UserPrefs p = new UserPrefs();

            p.UserID = int.Parse(row[0].ToString());
            p.CanAdopt = int.Parse(row[1].ToString());
            p.CanUpload = int.Parse(row[2].ToString());
            p.PrefTypeID = int.Parse(row[3].ToString());
            p.PrefRaceID = int.Parse(row[4].ToString());

            return p;
        }

        public async Task<UserPrefs> GetPrefsAsync(int userID)
        {
            Dictionary<string, object> f = new Dictionary<string, object>();
            f.Add("UserID", userID);

            List<UserPrefs> list = (List<UserPrefs>)await SelectAllAsync(f);
            if (list.Count == 1)
                return list[0];

            return null;
        }

        public async Task<UserPrefs> SavePrefsAsync(UserPrefs p)
        {
            UserPrefs existing = await GetPrefsAsync(p.UserID);

            if (existing == null)
            {
                Dictionary<string, object> v = new Dictionary<string, object>()
                {
                    { "UserID", p.UserID },
                    { "CanAdopt", p.CanAdopt },
                    { "CanUpload", p.CanUpload },
                    { "PrefTypeID", p.PrefTypeID },
                    { "PrefRaceID", p.PrefRaceID }
                };

                return await base.InsertGetObjAsync(v);
            }
            else
            {
                Dictionary<string, object> v = new Dictionary<string, object>()
                {
                    { "CanAdopt", p.CanAdopt },
                    { "CanUpload", p.CanUpload },
                    { "PrefTypeID", p.PrefTypeID },
                    { "PrefRaceID", p.PrefRaceID }
                };

                Dictionary<string, object> f = new Dictionary<string, object>()
                {
                    { "UserID", p.UserID }
                };

                await base.UpdateAsync(v, f);
                return await GetPrefsAsync(p.UserID);
            }
        }
    }
}
