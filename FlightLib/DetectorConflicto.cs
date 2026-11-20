using System;
using System.Collections.Generic;

namespace FlightLib
{
    public class ConflictDetector
    {
        FlightPlanList lista; //llista de vuelos 

        double distanciaSeguridad2;  //distancia minima

        int lenght;
         
        public ConflictDetector(FlightPlanList lista, double distanciaSeguridad)
        {
            this.lista = lista;
            this.distanciaSeguridad2 = distanciaSeguridad * distanciaSeguridad;
            lenght = lista.GetLenght();
        }
        public bool HayConflicto(FlightPlan a, FlightPlan b)
        {
            double ta = 60 * (a.Distance(a) / a.GetVelocidad());
            double tb = 60 * (b.Distance(b) / b.GetVelocidad());

            double dt = Math.Min(ta, tb); //el temps en el que analitzem

            Position pa = a.GetcurrentPosition();
            Position pb = b.GetcurrentPosition();

            double rx = pb.GetX() - pa.GetX();      //posicio relativa de b respecte á a
            double ry = pb.GetY() - pa.GetY();

            double wx = b.GetVelocidadX() - a.GetVelocidadX();    //velocitat relativa
            double wy = b.GetVelocidadY() - a.GetVelocidadY();

            double ww = wx * wx + wy * wy; //v relativa al quadrat
            double tm = Math.Max(0, Math.Min(dt, -(rx * wx + ry * wy) / ww)); //el temps en el que es tallen les trajectories 

            double mx = rx + wx * tm; //quan es tallen trobes la posició
            double my = ry + wy * tm;
            
            return (mx*mx + my*my) < distanciaSeguridad2;
        }

        //llista de parells de avions en conflicte, id's
        public List<string> ConflictosDetectados()
        {
            List<string> res = new List<string>();
            for (int i = 0; i < lenght; i++)
                for (int j = i + 1; j < lenght; j++)
                {
                    FlightPlan a = lista.GetFlightPlan(i);
                    FlightPlan b = lista.GetFlightPlan(j);
                    if (HayConflicto(a, b)) 
                        res.Add(a.Getid() + " - " + b.Getid());
                }
            return res;
        }

        //comprova si un vol esta en conflict amb alguno de los demas
        private bool ConflictoConAlguno(FlightPlan p)
        {
            for (int i = 0; i < lenght; i++)
            {
                FlightPlan otro = lista.GetFlightPlan(i);
                if (otro != p && HayConflicto(p, otro)) return true;
            }
            return false;
        }

        //  Prueba velocidades cada vez mas alejadas de la original (alternando menos/mas, pasos del 5%)
        //  hasta encontrar una que no genere conflicto. Se queda con la de menor cambio.
        private bool CambiarVelocidad(FlightPlan p)
        {
            double original = p.GetVelocidad();
            for (int k = 1; k <= 40; k++)
            {
                double[] candidatas = { original * (1 - 0.05 * k), original * (1 + 0.05 * k) };
                foreach (double v in candidatas)
                {
                    if (v <= original * 0.1) continue;          //  No bajamos de un 10% de la velocidad original
                    p.SetVelocidad(v);
                    if (!ConflictoConAlguno(p)) return true;
                }
            }
            p.SetVelocidad(original);                           //  Sin solucion: dejamos la velocidad como estaba
            return false;
        }

        //  Resuelve todos los conflictos cambiando la velocidad de un vuelo de cada par. Devuelve true si no queda ninguno
        public bool ResolverConflictos()
        {
            for (int i = 0; i < lenght; i++)
                for (int j = i + 1; j < lenght; j++)
                {
                    FlightPlan a = lista.GetFlightPlan(i);
                    FlightPlan b = lista.GetFlightPlan(j);
                    if (HayConflicto(a, b))
                    {
                        if (!CambiarVelocidad(b)) CambiarVelocidad(a);   //  Primero probamos con b, si no con a
                    }
                }
            return false;
        }
    }
}

//prova