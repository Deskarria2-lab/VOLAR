
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
