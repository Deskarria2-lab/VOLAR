// FlightPlanData emmagatzema una còpia de les dades d'un avió en un instant concret.
// Permet conservar les posicions i velocitats de cada avió al llarg de la simulació.
namespace FlightLib.Base_de_Datos
{
    public class FlightPlanData
    {
        public string UserEmail { get; set; }
        public string FlightId { get; set; }

        public double Speed { get; set; }
        public double InitialSpeed { get; set; }

        public double InitialX { get; set; }
        public double InitialY { get; set; }

        public double FinalX { get; set; }
        public double FinalY { get; set; }

        public double CurrentX { get; set; }
        public double CurrentY { get; set; }
    }
}
