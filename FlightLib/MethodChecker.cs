using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    internal class MethodChecker
    {
        public bool Typev(object variable, Type formato)
        {
            if (variable.GetType() == formato) return true;
            else return false;
        }
        public int vlim(double velocidad) 
        {
            if (velocidad < 10) return 0;
            else return 1;
        }

        public void verror(int vlim) 
        {
            if (vlim == 1) throw new ArgumentException("La velocidad no puede ser negativa");
        }
    }
}
