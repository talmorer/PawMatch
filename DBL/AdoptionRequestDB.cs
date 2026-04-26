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
        private string connString = "server=localhost;database=pawmatch;uid=root;pwd=;";

        public async Task InsertAsync(AdoptionRequest req)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sql = "INSERT INTO adoption_request (PetID, AdopterID, Status, RequestDate) VALUES (@p, @a, @s, NOW())";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@p", req.PetID);
                    cmd.Parameters.AddWithValue("@a", req.AdopterID);
                    cmd.Parameters.AddWithValue("@s", "Pending");
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateStatusAsync(int requestId, string status)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sql = "UPDATE adoption_request SET Status = @s WHERE RequestID = @id";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@s", status);
                    cmd.Parameters.AddWithValue("@id", requestId);
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
                SELECT a.RequestID, a.PetID, a.AdopterID, a.Status, a.RequestDate,
                       p.PetName, u.FirstName, u.Phone
                FROM adoption_request a
                INNER JOIN pets p ON a.PetID = p.PetID
                INNER JOIN users u ON a.AdopterID = u.UserID
                WHERE p.UpdateUserID = @ownerId AND a.Status = 'Pending'";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ownerId", ownerId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            AdoptionRequest r = new AdoptionRequest();
                            r.RequestID = reader.GetInt32("RequestID");
                            r.PetID = reader.GetInt32("PetID");
                            r.AdopterID = reader.GetInt32("AdopterID");
                            r.Status = reader.GetString("Status");
                            r.RequestDate = reader.GetDateTime("RequestDate");
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
    }
}