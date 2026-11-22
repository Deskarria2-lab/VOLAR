
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using FlightLib;

namespace FlightLib.Base_de_Datos
{
    public class FlightPlanRepository
    {
        private DataBase database;

        public FlightPlanRepository(DataBase database)
        {
            this.database = database;
        }

        // CREATE TABLE
        public void CrearTabla()
        {
            string query = @"
                CREATE TABLE IF NOT EXISTS FlightPlans
                (
                    UserEmail TEXT NOT NULL,
                    FlightId TEXT NOT NULL,
                    Speed REAL NOT NULL,
                    InitialSpeed REAL NOT NULL,
                    InitialX REAL NOT NULL,
                    InitialY REAL NOT NULL,
                    FinalX REAL NOT NULL,
                    FinalY REAL NOT NULL,
                    CurrentX REAL NOT NULL,
                    CurrentY REAL NOT NULL,
                    PRIMARY KEY (UserEmail, FlightId)
                )";

            SQLiteCommand command =
                new SQLiteCommand(query, database.Connection);

            command.ExecuteNonQuery();
        }

        // DELETE + INSERT
        // Guarda els plans i substitueix els anteriors de l'usuari
        public void GuardarPlanes(string email, FlightPlanList lista)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correu no és vàlid.");

            if (lista == null)
                throw new ArgumentNullException("lista");

            SQLiteConnection cnx = database.Connection;

            SQLiteTransaction transaction = cnx.BeginTransaction();

            try
            {
                // DELETE: elimina els plans anteriors de l'usuari
                string queryDelete =
                    "DELETE FROM FlightPlans WHERE UserEmail = @Email";

                SQLiteCommand commandDelete =
                    new SQLiteCommand(queryDelete, cnx, transaction);

                commandDelete.Parameters.AddWithValue("@Email", email);

                commandDelete.ExecuteNonQuery();

                // INSERT: introdueix els plans actuals
                string queryInsert = @"
                    INSERT INTO FlightPlans
                    (
                        UserEmail, FlightId,
                        Speed, InitialSpeed,
                        InitialX, InitialY,
                        FinalX, FinalY,
                        CurrentX, CurrentY
                    )
                    VALUES
                    (
                        @Email, @Id,
                        @Speed, @InitialSpeed,
                        @InitialX, @InitialY,
                        @FinalX, @FinalY,
                        @CurrentX, @CurrentY
                    )";

                SQLiteCommand commandInsert =
                    new SQLiteCommand(queryInsert, cnx, transaction);

                for (int i = 0; i < lista.GetNPlanes(); i++)
                {
                    FlightPlan plan = lista.GetFlightPlan(i);

                    if (plan != null)
                    {
                        commandInsert.Parameters.Clear();

                        commandInsert.Parameters.AddWithValue(
                            "@Email", email);

                        commandInsert.Parameters.AddWithValue(
                            "@Id", plan.Getid());

                        commandInsert.Parameters.AddWithValue(
                            "@Speed", plan.GetVelocidad());

                        commandInsert.Parameters.AddWithValue(
                            "@InitialSpeed", plan.GetVelocidadInicial());

                        commandInsert.Parameters.AddWithValue(
                            "@InitialX",
                            plan.GetinitialPosition().GetX());

                        commandInsert.Parameters.AddWithValue(
                            "@InitialY",
                            plan.GetinitialPosition().GetY());

                        commandInsert.Parameters.AddWithValue(
                            "@FinalX",
                            plan.GetfinalPosition().GetX());

                        commandInsert.Parameters.AddWithValue(
                            "@FinalY",
                            plan.GetfinalPosition().GetY());

                        commandInsert.Parameters.AddWithValue(
                            "@CurrentX",
                            plan.GetcurrentPosition().GetX());

                        commandInsert.Parameters.AddWithValue(
                            "@CurrentY",
                            plan.GetcurrentPosition().GetY());

                        commandInsert.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // SELECT
        // Recupera les dades dels plans d'un usuari
        public List<FlightPlanData> CargarPlanes(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correu no és vàlid.");

            List<FlightPlanData> planes =
                new List<FlightPlanData>();

            DataTable dt = new DataTable();

            string query =
                "SELECT * FROM FlightPlans WHERE UserEmail = @Email";

            SQLiteDataAdapter adp =
                new SQLiteDataAdapter(query, database.Connection);

            adp.SelectCommand.Parameters.AddWithValue("@Email", email);

            int rows = adp.Fill(dt);

            int i = 0;

            while (i < dt.Rows.Count)
            {
                FlightPlanData data = new FlightPlanData();

                data.UserEmail =
                    Convert.ToString(dt.Rows[i]["UserEmail"]);

                data.FlightId =
                    Convert.ToString(dt.Rows[i]["FlightId"]);

                data.Speed =
                    Convert.ToDouble(dt.Rows[i]["Speed"]);

                data.InitialSpeed =
                    Convert.ToDouble(dt.Rows[i]["InitialSpeed"]);

                data.InitialX =
                    Convert.ToDouble(dt.Rows[i]["InitialX"]);

                data.InitialY =
                    Convert.ToDouble(dt.Rows[i]["InitialY"]);

                data.FinalX =
                    Convert.ToDouble(dt.Rows[i]["FinalX"]);

                data.FinalY =
                    Convert.ToDouble(dt.Rows[i]["FinalY"]);

                data.CurrentX =
                    Convert.ToDouble(dt.Rows[i]["CurrentX"]);

                data.CurrentY =
                    Convert.ToDouble(dt.Rows[i]["CurrentY"]);

                planes.Add(data);

                i = i + 1;
            }

            return planes;
        }

        // Reconstrueix la llista de plans de vol
        public FlightPlanList CarregarFlightPlanList(string email)
        {
            List<FlightPlanData> dades = CargarPlanes(email);

            FlightPlanList lista = new FlightPlanList();

            foreach (FlightPlanData data in dades)
            {
                FlightPlan plan = new FlightPlan(
                    data.FlightId,
                    data.InitialX,
                    data.InitialY,
                    data.FinalX,
                    data.FinalY,
                    data.InitialSpeed
                );

                // Recupera la posició actual
                plan.SetcurrentPosition(
                    new Position(data.CurrentX, data.CurrentY));

                // Recupera la velocitat actual
                plan.SetVelocidad(data.Speed);

                lista.AddFlightPlan(plan);
            }

            return lista;
        }
    }
}
