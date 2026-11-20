using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

// Esta clase se encarga de hacer consultas a la base de datos

namespace FlightLib.Base_de_Datos
{
    internal class UserRepository
    {
        private readonly DataBase database;

        public UserRepository(DataBase database) 
        {
            this.database = database;
        }
        public void AddUsers(User user)
        {
            string query =
                @"INSERT INTO users (email, username, hash, salt) 
                VALUES (@email, @username, @hash, @salt);";

            using (SQLiteCommand cmd = new SQLiteCommand(query, database.Connection))
            {
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@hash", user.Hash);
                cmd.Parameters.AddWithValue("@salt", user.Salt);

                cmd.ExecuteNonQuery();
            }
        }
        public DataTable GetUsers()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM users";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, database.Connection);
            return dt;
        }
        public User GetByEmail(string email) 
        {
            string query =
                @"SELECT email, username, hash, salt
                  FROM users
                  WHERE email = @email;
                ";
            using (SQLiteCommand cmd = new SQLiteCommand(query, database.Connection)) 
            {
                cmd.Parameters.AddWithValue("@email", email);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) { return null; }

                    return new User 
                    {
                        Email = reader["email"].ToString(),
                        Username = reader["username"].ToString(),
                        Hash = reader["hash"].ToString(),
                        Salt = reader["salt"].ToString()
                    };
                }
            }
        }
    }
}
