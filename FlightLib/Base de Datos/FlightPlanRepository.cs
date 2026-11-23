
using System;
using System.Collections.Generic;
using System.Data.SQLite;

//Aquesta classe només gestiona les operacions SQL (guarda i recupera de l'historial de la simulació)
//No crea ni modifica l'Stack: rep els estats que li passa FlightPlanHistory i els guarda a la base de dades

namespace FlightLib.Base_de_Datos
{
    public class FlightPlanRepository
    {
        private DataBase database;

        public FlightPlanRepository(DataBase database)
        {
            this.database = database;
        }

        // Crea la taula on es guardarà l'historial de la simulació
        public void CrearTabla()
        {
            string consulta = @"
                CREATE TABLE IF NOT EXISTS FlightPlanHistory
                (
                    UserEmail TEXT NOT NULL,
                    FlightId TEXT NOT NULL,
                    StepNumber INTEGER NOT NULL,
                    Speed REAL NOT NULL,
                    InitialSpeed REAL NOT NULL,
                    InitialX REAL NOT NULL,
                    InitialY REAL NOT NULL,
                    FinalX REAL NOT NULL,
                    FinalY REAL NOT NULL,
                    CurrentX REAL NOT NULL,
                    CurrentY REAL NOT NULL
                );";

            using (SQLiteCommand comando = new SQLiteCommand(consulta, database.Connection))
            {
                comando.ExecuteNonQuery();
            }
        }

        // Guarda tot l'historial d'un usuari
        // Els estats han d'arribar ordenats del més antic al més recent
        public void GuardarHistorial(string email, List<List<FlightPlanData>> estados)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("El correu de l'usuari no és vàlid.");
            }

            if (estados == null || estados.Count == 0)
            {
                throw new ArgumentException("No hi ha cap estat per guardar.");
            }

            using (SQLiteTransaction transaccion = database.Connection.BeginTransaction())
            {
                try
                {
                    // Eliminem l'historial anterior d'aquest usuari per evitar duplicats
                    string eliminar = @"
                        DELETE FROM FlightPlanHistory
                        WHERE UserEmail = @email;";

                    using (SQLiteCommand comandoEliminar = new SQLiteCommand(eliminar,database.Connection,transaccion))
                    {
                        comandoEliminar.Parameters.AddWithValue("@email", email);
                        comandoEliminar.ExecuteNonQuery();
                    }

                    // Inserim tots els estats de la simulació
                    string insertar = @"
                        INSERT INTO FlightPlanHistory
                        (
                            UserEmail,
                            FlightId,
                            StepNumber,
                            Speed,
                            InitialSpeed,
                            InitialX,
                            InitialY,
                            FinalX,
                            FinalY,
                            CurrentX,
                            CurrentY
                        )
                        VALUES
                        (
                            @email,
                            @flightId,
                            @stepNumber,
                            @speed,
                            @initialSpeed,
                            @initialX,
                            @initialY,
                            @finalX,
                            @finalY,
                            @currentX,
                            @currentY
                        );";

                    for (int i = 0; i < estados.Count; i++)
                    {
                        List<FlightPlanData> estado = estados[i];

                        foreach (FlightPlanData dades in estado)
                        {
                            using (SQLiteCommand comandoInsertar =
                                new SQLiteCommand(
                                    insertar,
                                    database.Connection,
                                    transaccion))
                            {
                                comandoInsertar.Parameters.AddWithValue("@email", email);
                                comandoInsertar.Parameters.AddWithValue("@flightId", dades.FlightId);
                                comandoInsertar.Parameters.AddWithValue("@stepNumber", i);
                                comandoInsertar.Parameters.AddWithValue("@speed", dades.Speed);
                                comandoInsertar.Parameters.AddWithValue("@initialSpeed", dades.InitialSpeed);
                                comandoInsertar.Parameters.AddWithValue("@initialX", dades.InitialX);
                                comandoInsertar.Parameters.AddWithValue("@initialY", dades.InitialY);
                                comandoInsertar.Parameters.AddWithValue("@finalX", dades.FinalX);
                                comandoInsertar.Parameters.AddWithValue("@finalY", dades.FinalY);
                                comandoInsertar.Parameters.AddWithValue("@currentX", dades.CurrentX);
                                comandoInsertar.Parameters.AddWithValue("@currentY", dades.CurrentY);
                                comandoInsertar.ExecuteNonQuery();
                            }
                        }
                    }

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        // Recupera tots els estats d'un usuari en ordre cronològic per reconstruir la simulació
        public List<List<FlightPlanData>> CargarHistorial(string email)
        {
            List<List<FlightPlanData>> estados =
                new List<List<FlightPlanData>>();

            string consulta = @"
                SELECT
                    FlightId,
                    StepNumber,
                    Speed,
                    InitialSpeed,
                    InitialX,
                    InitialY,
                    FinalX,
                    FinalY,
                    CurrentX,
                    CurrentY
                FROM FlightPlanHistory
                WHERE UserEmail = @email
                ORDER BY StepNumber ASC, FlightId ASC;";

            using (SQLiteCommand comando = new SQLiteCommand(consulta, database.Connection))
            {
                comando.Parameters.AddWithValue("@email", email);

                using (SQLiteDataReader lector = comando.ExecuteReader())
                {
                    int pasoActual = -1;
                    List<FlightPlanData> estadoActual = null;

                    while (lector.Read())
                    {
                        int numeroPaso = Convert.ToInt32(lector["StepNumber"]);

                        // Quan canviem de pas, comencem un estat nou
                        if (numeroPaso != pasoActual)
                        {
                            estadoActual = new List<FlightPlanData>();
                            estados.Add(estadoActual);
                            pasoActual = numeroPaso;
                        }

                        FlightPlanData dades = new FlightPlanData();

                        dades.FlightId = Convert.ToString(lector["FlightId"]);
                        dades.Speed = Convert.ToDouble(lector["Speed"]);
                        dades.InitialSpeed = Convert.ToDouble(lector["InitialSpeed"]);
                        dades.InitialX = Convert.ToDouble(lector["InitialX"]);
                        dades.InitialY = Convert.ToDouble(lector["InitialY"]);
                        dades.FinalX = Convert.ToDouble(lector["FinalX"]);
                        dades.FinalY = Convert.ToDouble(lector["FinalY"]);
                        dades.CurrentX = Convert.ToDouble(lector["CurrentX"]);
                        dades.CurrentY = Convert.ToDouble(lector["CurrentY"]);

                        estadoActual.Add(dades);
                    }
                }
            }

            return estados;
        }
    }
}
