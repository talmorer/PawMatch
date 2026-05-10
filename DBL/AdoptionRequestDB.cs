using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DBL
{
    public class AdoptionRequestDB : BaseDB<AdoptionRequest>
    {
        protected override string GetTableName()
        {
            return "adoption_request";
        }

        protected override string GetPrimaryKeyName()
        {
            return "PetID";
        }

        protected override async Task<AdoptionRequest> CreateModelAsync(object[] row)
        {
            AdoptionRequest r = new AdoptionRequest();

            if (row == null || row.Length < 4)
                return null;

            r.PetID = int.Parse(row[0].ToString());
            r.UserRequstingID = int.Parse(row[1].ToString());
            r.RequestDate = row[2]?.ToString();
            r.IsAdopted = int.Parse(row[3].ToString());

            if (row.Length > 4)
            {
                r.PetName = row[4]?.ToString();
                r.AdopterFirstName = row[5]?.ToString();
                r.AdopterPhone = row[6]?.ToString();
            }

            return r;
        }

        public async Task<AdoptionRequest> InsertAsync(AdoptionRequest req)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "PetID", req.PetID },
                { "UserRequstingID", req.UserRequstingID },
                { "RequestDate", req.RequestDate },
                { "IsAdopted", req.IsAdopted }
            };
            return (AdoptionRequest)await base.InsertGetObjAsync(fillValues);
        }

        public async Task<int> UpdateStatusAsync(int petId, int requestingUserId, int isAdoptedStatus)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            fillValues.Add("IsAdopted", isAdoptedStatus);

            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("PetID", petId);
            filterValues.Add("UserRequstingID", requestingUserId);

            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<List<AdoptionRequest>> GetRequestsForOwnerAsync(int ownerId)
        {
            string sql = @"
            SELECT a.PetID, a.UserRequstingID, a.RequestDate, a.IsAdopted,
                   p.PetName, u.FirstName, u.Phone
            FROM adoption_request a
            INNER JOIN pets p ON a.PetID = p.PetID
            INNER JOIN users u ON a.UserRequstingID = u.UserID
            WHERE p.UpdateUserID = @ownerId AND a.IsAdopted = 0";

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ownerId", ownerId);

            return (List<AdoptionRequest>)await SelectAllAsync(sql, p);
        }

        public async Task<AdoptionRequest> GetRequestByUserAndPetAsync(int userId, int petId)
        {
            string sql = "SELECT * FROM adoption_request WHERE UserRequstingID = @u AND PetID = @p LIMIT 1";

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("u", userId);
            p.Add("p", petId);

            List<AdoptionRequest> list = (List<AdoptionRequest>)await SelectAllAsync(sql, p);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        public async Task<List<AdoptionRequest>> GetRequestsByAdopterAsync(int userId)
        {
            string sql = @"
            SELECT a.PetID, a.UserRequstingID, a.RequestDate, a.IsAdopted,
                   p.PetName, u.FirstName, u.Phone
            FROM adoption_request a
            INNER JOIN pets p ON a.PetID = p.PetID
            INNER JOIN users u ON a.UserRequstingID = u.UserID
            WHERE a.UserRequstingID = @userId
            ORDER BY a.RequestDate DESC";

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("userId", userId);

            return (List<AdoptionRequest>)await SelectAllAsync(sql, p);
        }
    }
}