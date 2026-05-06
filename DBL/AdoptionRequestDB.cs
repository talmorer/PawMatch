using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DBL
{
    public class AdoptionRequestDB
    {
        private string connString = "server=localhost;database=project;uid=root;pwd=;";

        public async Task InsertAsync(AdoptionRequest req)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sql = "INSERT INTO adoption_request (PetID, UserRequstingID, RequestDate, IsAdopted) VALUES (@p, @u, @d, @i)";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@p", req.PetID);
                    cmd.Parameters.AddWithValue("@u", req.UserRequstingID);
                    cmd.Parameters.AddWithValue("@d", req.RequestDate);
                    cmd.Parameters.AddWithValue("@i", req.IsAdopted);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateStatusAsync(int petId, int requestingUserId, int isAdoptedStatus)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sql = "UPDATE adoption_request SET IsAdopted = @status WHERE PetID = @p AND UserRequstingID = @u";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@status", isAdoptedStatus);
                    cmd.Parameters.AddWithValue("@p", petId);
                    cmd.Parameters.AddWithValue("@u", requestingUserId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<AdoptionRequest>> GetRequestsForOwnerAsync(int ownerId)
        {
            List<AdoptionRequest> list = new List<AdoptionRequest>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sql = @"
                SELECT a.PetID, a.UserRequstingID, a.RequestDate, a.IsAdopted,
                       p.PetName, u.FirstName, u.Phone
                FROM adoption_request a
                INNER JOIN pets p ON a.PetID = p.PetID
                INNER JOIN users u ON a.UserRequstingID = u.UserID
                WHERE p.UpdateUserID = @ownerId AND a.IsAdopted = 0";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ownerId", ownerId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            AdoptionRequest r = new AdoptionRequest();
                            r.PetID = reader.GetInt32("PetID");
                            r.UserRequstingID = reader.GetInt32("UserRequstingID");
                            r.RequestDate = reader.GetString("RequestDate");
                            r.IsAdopted = reader.GetInt32("IsAdopted");
                            r.PetName = reader.GetString("PetName");
                            r.AdopterFirstName = reader.GetString("FirstName");
                            r.AdopterPhone = reader.GetString("Phone");
                            list.Add(r);
                        }
                    }
                }
            }
            return list;
        }

        public async Task<AdoptionRequest> GetRequestByUserAndPetAsync(int userId, int petId)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sql = "SELECT * FROM adoption_request WHERE UserRequstingID = @u AND PetID = @p LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@u", userId);
                    cmd.Parameters.AddWithValue("@p", petId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            AdoptionRequest r = new AdoptionRequest();
                            r.PetID = reader.GetInt32("PetID");
                            r.UserRequstingID = reader.GetInt32("UserRequstingID");
                            r.RequestDate = reader.GetString("RequestDate");
                            r.IsAdopted = reader.GetInt32("IsAdopted");
                            return r;
                        }
                    }
                }
            }
            return null;
        }
    }
}