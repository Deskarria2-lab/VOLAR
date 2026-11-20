using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

// Esta Base sirve para gestionar la conexion a la base de datos 

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
        internal SQLiteConnection Connection 
        {
            get 
            {
                if (this.cnx == null) 
                {
                    throw new InvalidOperationException(
                        "La conexion con la base de datos no esta abierta");
                }

                return cnx;
            }
        }
    }
}
