using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlanList
    {
        FlightPlan[] vector;
        int number;

        public FlightPlanList() 
        {
            this.vector = new FlightPlan[10];
            this.number = 0;
        }
        public int AddFlightPlan(FlightPlan p) 
        {
            if (number >= 10) return -1;
            else 
            {
                vector[number] = p;
                number++;
                return 0;
            }
        }
        public FlightPlan GetFlightPlan(int number)
        {
            if(number < 0 || number >= this.number) return null;
            else return vector[number];
        }
        public void Mover(double tiempo) 
        {
            for (int i = 0; i < this.number; i++) vector[i].Mover(tiempo);
        }
        public void EscribeConsola() 
        {
            for (int i = 0; i < this.number; i++) vector[i].EscribeConsola();
        }
    }
}
