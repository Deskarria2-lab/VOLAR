using FlightLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Aquesta classe s'encarrega de gestionar la lectura i escriptura de fitxers
// Proporciona mètodes per carregar els vols des d'un fitxer
// També manté un estat intern que indica l'èxit o el fracàs de l'última operació amb el fitxer

namespace FlightLib
{
    public class GesitonFicheros
    {
        MethodChecker checker;
        FlightPlanList planList;
        string file;
        int i;

        // Inicialitza el gestor amb la ruta del fitxer que s'utilitzarà.
        public void GestionarFicheros(string file)
        {
            this.checker = new MethodChecker();
            this.file = file;
            i = 0;
        }
        
        // Llegeix els vols del fitxer, valida les dades i els retorna en una llista.
        public FlightPlanList CargarVuelosDesdeFichero()
        {
            using (StreamReader lector = new StreamReader(file))
            {

                string Linea = lector.ReadLine();
                try 
                {
                    FlightPlanList plan = new FlightPlanList();
                    while (Linea != null)
                    {
                        if (Linea == null)
                        {
                            i = -1;
                            return null;
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

                                int resultat = 0;

                                if (resultat == 0)
                                {
                                    plan.AddFlightPlan(nouVol);
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

                    return plan;
                }catch{ i = 4; return null; }
            }
        }

        // Desa al fitxer els plans de vol rebuts.
        public void GuardarVuelos(string ruta, IEnumerable<FlightPlan> plan) 
        {
            try 
            {
                using (StreamWriter escritor = new StreamWriter(file)) 
                {
                    foreach (FlightPlan vuelo in plan) 
                    {

                        escritor.WriteLine(string.Join(" ",
                            vuelo.Getid(),
                            vuelo.GetVelocidad(),
                            vuelo.GetcurrentPosition(),
                            vuelo.GetfinalPosition()));
                    }
                }
            }
            catch
            {
            }
        }

        // Retorna un missatge que descriu l'estat de l'última operació amb el fitxer.
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
