using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class DataBase
    {
        SQLiteConnection cnx;

        public void Iniciar() 
        {
            string dataSource = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "userBD.db");
            string connectionString = $"Data Source={dataSource};Version=3;";
            this.cnx = new SQLiteConnection(connectionString);
            this.cnx.Open();
        }

        public void Cerrar() 
        {
            this.cnx.Close();
        }

        public DataTable GetUsers()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM users";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, this.cnx);
            return dt;
        }

        public void AddUsers(string email, string username, string hash, string salt) 
        {
            string query = 
                @"INSERT INTO users (email, username, hash, salt) 
                VALUES (@email, @username, @hash, @salt);";

            using (SQLiteCommand cmd = new SQLiteCommand(query, this.cnx)) 
            {
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.Parameters.AddWithValue("@salt", salt);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
