using FlightLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlightLib
{
    public class GesitonFicheros
    {
        MethodChecker checker;
        FlightPlanList planList;
        string file;
        int i;

        public void GestionarFicheros(string file)
        {
            this.checker = new MethodChecker();
            this.file = file;
            i = 0;
        }
        
        public void CargarVuelosDesdeFichero()
        {
            using (StreamReader lector = new StreamReader(file))
            {

                string Linea = lector.ReadLine();
                try 
                {
                    while (Linea != null)
                    {
                        if (Linea == null)
                        {
                            i = -1;
                            return;
                        }
                        else
                        {
                            string[] trozos = Linea.Split(' ');
                            string id = trozos[0];
                            double vel = Convert.ToDouble(trozos[1]);
                            double Xinicial = Convert.ToDouble(trozos[2]);
                            double Yinicial = Convert.ToDouble(trozos[3]);
                            double Xfinal = Convert.ToDouble(trozos[4]);
                            double Yfinal = Convert.ToDouble(trozos[5]);

                            if (checker.check_id(id) == "0" && checker.check_vel(vel) == "0" && checker.check_pos(Xinicial, Yinicial, Xfinal, Yfinal) == "0")
                            {
                                FlightPlan nouVol = new FlightPlan(id, Xinicial, Yinicial, Xfinal, Yfinal, vel);

                                int resultat = planList.AddFlightPlan(nouVol);

                                if (resultat == 0)
                                {
                                    i = 1;
                                }
                                else
                                {
                                    i = 2;
                                }
                            }
                            else
                            {
                                i = 3;
                            }

                        }
                        Linea = lector.ReadLine();
                    }
                }catch{ i = 4; }
            }
        }

        public string EstadoFichero() 
        {
            if (i == -1) return "El fitxer està buit.";
            else if (i == 1) return "El vol esta en el Flight plan list correctament";
            else if (i == 2) return "La llista de vols està plena.";
            else if (i == 3) return "Les dades del vol no són correctes.";
            else if (i == 4) return "El fichero no sigue el formato esperado";
            else return "Error inexperado";
        }
    }
}
