using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Esta clase se encarga de hacer consultas a la base de datos

namespace FlightLib.Base_de_Datos
{
    internal class UserRepository
    {
        private readonly DataBase database;
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
    }
}
