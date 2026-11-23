using System;
using System.Collections.Generic;

namespace FlightLib
{
    public class ConflictDetector
    {
        FlightPlanList lista; //llista de vuelos 
        double distanciaSeguridad;  //distancia minima

        public ConflictDetector(FlightPlanList lista, double distanciaSeguridad)
        {
            this.lista = lista;
            this.distanciaSeguridad = distanciaSeguridad;
        }

        private int NumVuelos() //conta els avions que hi ha a la llista de vols
        {
            int n = 0;
            while (lista.GetFlightPlan(n) != null)
            {
                n++;
            }
            return n;
        }
        public bool HayConflicto(FlightPlan a, FlightPlan b)
        {
            double ax = a.GetcurrentPosition().GetX();
            double ay = a.GetcurrentPosition().GetY();
            double adx = a.GetfinalPosition().GetX() - ax;
            double ady = a.GetfinalPosition().GetY() - ay;
            double adist = Math.Sqrt(adx * adx + ady * ady);
            double av = a.GetVelocidad();
            double avx = adx / adist * av;
            double avy = ady / adist * av;
            double ta = adist / av;

            double bx = b.GetcurrentPosition().GetX();
            double by = b.GetcurrentPosition().GetY();
            double bdx = b.GetfinalPosition().GetX() - bx;
            double bdy = b.GetfinalPosition().GetY() - by;
            double bdist = Math.Sqrt(bdx * bdx + bdy * bdy);
            double bv = b.GetVelocidad();
            double bvx = bdx / bdist * bv;
            double bvy = bdy / bdist * bv;
            double tb = bdist / bv;

            double dt = Math.Min(ta, tb); //el temps en el que analitzem

            double rx = bx - ax;      //posicio relativa de b respecte á a
            double ry = by - ay;
            double wx = bvx - avx;    //velocitat relativa
            double wy = bvy - avy;

            double ww = wx * wx + wy * wy;

            double tm = 0;

            if (ww > 1e-12)
            {
                tm = -(rx * wx + ry * wy) / ww;

                tm = Math.Max(0, Math.Min(dt, tm));
            }

            double mx = rx + wx * tm; //quan es tallen trobes la posició
            double my = ry + wy * tm;

            if (Math.Sqrt(mx * mx + my * my) <= distanciaSeguridad)
            {
                return true;
            }
            return false;
        }

        //llista de parells de avions en conflicte, id's
        public List<FlightPlan> ConflictosDetectados()
        {
            List<FlightPlan> res = new List<FlightPlan>();
            int n = NumVuelos();
            for (int i = 0; i < n; i++) 
            {
                for (int j = i + 1; j < n; j++)
                {
                    FlightPlan a = lista.GetFlightPlan(i);
                    FlightPlan b = lista.GetFlightPlan(j);
                    if (HayConflicto(a, b))
                    {
                        res.Add(a);
                        res.Add(b);
                    }

                }

            }
            return res;
        }

        //comprova si un vol esta en conflict amb alguno de los demas
        private bool ConflictoConAlguno(FlightPlan p)
        {
            int n = NumVuelos();
            for (int i = 0; i < n; i++)
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
        public List<FlightPlan> ResolverConflictos()
        {
            List<FlightPlan> cambiados = new List<FlightPlan>();     // aviones modificados, sin repetidos
            const int MaxPasadas = 50;                               // evita un bucle infinito si no converge

            for (int pasada = 0; pasada < MaxPasadas; pasada++)
            {
                bool hubo = false;           // ¿había algún conflicto en esta pasada?
                bool progreso = false;       // ¿se ha arreglado alguno?
                int n = NumVuelos();

                for (int i = 0; i < n; i++)
                {
                    FlightPlan a = lista.GetFlightPlan(i);           // se pide una sola vez por fila
                    for (int j = i + 1; j < n; j++)
                    {
                        FlightPlan b = lista.GetFlightPlan(j);
                        if (!HayConflicto(a, b)) continue;

                        hubo = true;
                        // Primero se prueba con b; si no sirve, con a
                        if (Intentar(b, a, cambiados) || Intentar(a, b, cambiados))
                            progreso = true;
                        // si ninguno puede, esa pareja se deja como está
                    }
                }

                if (!hubo || !progreso) break;    // no quedan conflictos, o los que quedan no tienen arreglo
            }

            return cambiados;
        }

        // Cambia la velocidad de x y comprueba que con ello deja de haber conflicto con 'otro'
        bool Intentar(FlightPlan x, FlightPlan otro, List<FlightPlan> cambiados)
        {
            double velAntes = x.GetVelocidad();

            if (!CambiarVelocidad(x)) return false;      // no se pudo cambiar

            if (HayConflicto(x, otro))                   // se cambió, pero no sirvió
            {
                x.SetVelocidad(velAntes);                // se deshace el cambio
                return false;
            }

            if (!cambiados.Contains(x)) cambiados.Add(x);
            return true;
        }
    }
}

//prova
