
using System;
using System.Collections.Generic;
using System.Linq;

//Aquesta classe crea stack per gurdar els estats de cada avio cada cop que s'efectua un cicle
//El primer estat és l'estat inicial i, després de cada moviment, s'afegeix un estat nou al Stack

namespace FlightLib.Base_de_Datos
{
    public class FlightPlanHistory
    {
        private Stack<List<FlightPlanData>> historial;

        public FlightPlanHistory()
        {
            historial = new Stack<List<FlightPlanData>>();
        }

        public int Count
        {
            get { return historial.Count; }
        }

        public void Iniciar(FlightPlanList lista)
        {
            historial.Clear();

            List<FlightPlanData> estatInicial = CrearEstat(lista);

            historial.Push(estatInicial);
        }

        // Registra un nou estat després d'un moviment.
        public void RegistrarPas(FlightPlanList lista)
        {
            List<FlightPlanData> nouEstat = CrearEstat(lista);

            historial.Push(nouEstat);
        }

        // Retorna una còpia de l'estat actual de tots els avions
        private List<FlightPlanData> CrearEstat(FlightPlanList lista)
        {
            List<FlightPlanData> estat = new List<FlightPlanData>();

            for (int i = 0; i < lista.GetNPlanes(); i++)
            {
                FlightPlan avio = lista.GetFlightPlan(i);

                if (avio != null)
                {
                    FlightPlanData dades = new FlightPlanData();

                    dades.FlightId = avio.Getid();

                    dades.InitialX = avio.GetinitialPosition().GetX();
                    dades.InitialY = avio.GetinitialPosition().GetY();

                    dades.FinalX = avio.GetfinalPosition().GetX();
                    dades.FinalY = avio.GetfinalPosition().GetY();

                    dades.CurrentX = avio.GetcurrentPosition().GetX();
                    dades.CurrentY = avio.GetcurrentPosition().GetY();

                    dades.Speed = avio.GetVelocidad();
                    dades.InitialSpeed = avio.GetVelocidadInicial();

                    estat.Add(dades);
                }
            }

            return estat;
        }

        // Retorna l'estat més recent de la simulació
        public List<FlightPlanData> ObtenerEstadoActual()
        {
            if (historial.Count == 0)
            {
                return null;
            }

            return historial.Peek();
        }

        // Retorna tots els estats ordenats del més antic al més recent
        public List<List<FlightPlanData>> ObtenerEstadosPerGuardar()
        {
            return historial.Reverse().ToList();
        }

        // Carrega a l'Stack els estats recuperats de la base de dades
        public void CargarHistorial(List<List<FlightPlanData>> estados)
        {
            historial.Clear();

            if (estados == null)
            {
                return;
            }

            foreach (List<FlightPlanData> estado in estados)
            {
                historial.Push(estado);
            }
        }

        // Retorna la llista d'avions a partir d'un estat per poder reconstruir l'última simulació
        public FlightPlanList ReconstruirPlanes(List<FlightPlanData> estado)
        {
            FlightPlanList lista = new FlightPlanList();

            if (estado == null)
            {
                return lista;
            }

            foreach (FlightPlanData dades in estado)
            {
                FlightPlan avio = new FlightPlan(
                    dades.FlightId,
                    dades.InitialX,
                    dades.InitialY,
                    dades.FinalX,
                    dades.FinalY,
                    dades.InitialSpeed
                );

                // Recuperem la posició i la velocitat de l'últim estat.
                avio.SetcurrentPosition(new Position(dades.CurrentX, dades.CurrentY));
                avio.SetVelocidad(dades.Speed);
                lista.AddFlightPlan(avio);
            }

            return lista;
        }

        // Desfa l'últim pas de la simulació --> recupera l'estat anterior
        public FlightPlanList DesferPas()
        {
            if (historial.Count <= 1)
            {
                return null;
            }

            // Eliminem l'estat actual
            historial.Pop();

            // Recuperem l'estat anterior
            List<FlightPlanData> estatAnterior = historial.Peek();

            // Reconstruïm els avions
            return ReconstruirPlanes(estatAnterior);
        }
    }

}
