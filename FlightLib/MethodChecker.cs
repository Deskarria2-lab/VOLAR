using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class MethodChecker
    {
        datas database = new datas();
        public string check_id(string id) 
        {
            if (id == "")
                return "No se ha introducido un identificador";
            string head_id = id.Substring(0, 2);
            head_id = head_id.ToUpper();
            if (!database.codecheck(head_id))
                return "El header del ID no es valido, no esta dentro de la base de datos";
            else
                return "0";
        }
        public string check_vel(double vel)
        {
            if (vel < 0) return "La velocidad no puede ser negativa"; 
            else if (vel == 0) return "La velocidad no puede ser nula";
            else return "0";
        }
        public string check_pos(double xo, double yo, double xf, double yf)
        {
            if (xo == xf && yo == yf)
                return "La posicion inicial y final no deben ser la misma, no hay vuelo!";
            else if ((xo < 0)) return "La cord x0 no debe ser negativa!";
            else if (yo < 0) return "La cord y0 no debe ser negativa!";
            else if (xf < 0) return "La cord xf no debe ser negativa!";
            else if (yf < 0) return "La cord yf no debe ser negativa!";
            else return "0";
        }

        public string check_dseg(double dseg)
        {
            if (dseg < 0) return "La distancia de seguridad no puede ser menor a 0!";
            else if (dseg == 0) return "La distancia de seguridad no puede ser  0!";
            else return "0";
        }
        public string check_tcicl(double tcicl)
        {
            if (tcicl < 0) return "El tiempo no puede ser menor a 0!";
            else if (tcicl == 0) return "El tiempo no puede ser 0!";
            else return "0";
        }
    }
}
