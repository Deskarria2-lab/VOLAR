using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlan
    {
        //////////////////////////////////////////////////////////////    Atributos   ////////////////////////////////////////////////////////////////////
        string id;                                                                            
        Position initialPosition;                                                              
        Position currentPosition;                                                              
        Position finalPosition;                                                                
        double velocidad;                                                                       
        double angulo;
        

        // Constructures
        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;                                                                       //  Inicializamos el Identificador
            this.currentPosition = new Position(cpx, cpy);                                      //  Inicializamos la pos Actual
            this.initialPosition = new Position(cpx, cpy);                                      //  Inicializamos la pos Inicial
            this.finalPosition = new Position(fpx, fpy);                                        //  Inicializamos la pos Final
            this.velocidad = velocidad;                                                         //  Inicializamos la velocidad
            angulo = Math.Atan((fpy - cpy) / (fpx - cpx));
        }

        public string Getid()                                                                   
        { return this.id; }
        public Position GetinitialPosition()                                                    
        { return this.initialPosition; }
        public Position GetcurrentPosition()                                                   
        { return this.currentPosition; }
        public Position GetfinalPosition()                                                      
        { return this.finalPosition; }
        public double GetVelocidad()                                                            
        { return this.velocidad; }
        //public double GetAngulo() 
        //{  return this.angulo; }
        public double GetVelocidadX() 
        { return velocidad * Math.Cos(angulo); }
        public double GetVelocidadY() 
        { return velocidad * Math.Sin(angulo); }

        public void SetVelocidad(double velocidad)
        // setter del atributo velocidad
        { this.velocidad = velocidad; }

        public void Mover(double tiempo)
        // Mueve el vuelo a la posición correspondiente a viajar durante el tiempo que se recibe como parámetro
        {
            //Calculamos la distancia recorrida en el tiempo dado
            double distancia = tiempo * this.velocidad / 60;

            //Calculamos las razones trigonométricas
            double hipotenusa = Math.Sqrt((finalPosition.GetX() - currentPosition.GetX()) * (finalPosition.GetX() - currentPosition.GetX()) + (finalPosition.GetY() - currentPosition.GetY()) * (finalPosition.GetY() - currentPosition.GetY()));
            double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;
            double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;

            //Caculamos la nueva posición del vuelo
            double x = currentPosition.GetX() + distancia * coseno;
            double y = currentPosition.GetY() + distancia * seno;

            Position nextPosition = new Position(x, y);

            if (currentPosition.Distancia(nextPosition) < hipotenusa)
            {
                currentPosition = nextPosition;
            }
            else
            {
                currentPosition = finalPosition;
            }
        }
        public bool EstaDestino() 
        {
            bool resultado = false;
            if(currentPosition == finalPosition) { resultado = true; }

            return resultado;
        }
        public bool Conflicto(FlightPlan b, double distanciaSeguridad) 
        {
            bool conflicto = false;

            if(this.currentPosition.Distancia(b.currentPosition) < distanciaSeguridad) 
            {
                conflicto = true;
            }

            return conflicto;
        }
        public void EscribeConsola()
        // escribe en consola los datos del plan de vuelo
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Datos del vuelo: ");
            Console.WriteLine("Identificador: {0}", id);
            Console.WriteLine("Velocidad: {0:F2}", velocidad);
            Console.WriteLine("Posición actual: ({0:F2},{1:F2})", currentPosition.GetX(), currentPosition.GetY());
            if(this.EstaDestino())
                Console.WriteLine("Ha llegado al desti");
            Console.WriteLine("******************************");
        }
    }
}
