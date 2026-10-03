using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////    Clase para gestionar el control de los aviones   ////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class FlightPlan
    {
        //////////////////////////////////////////////////////////////    Atributos   ////////////////////////////////////////////////////////////////////
        string id;                                                                              // identificador
        Position initialPosition;                                                               //  Posicion Inicial
        Position currentPosition;                                                               // posicion actual
        Position finalPosition;                                                                 // posicion final
        double velocidad;                                                                       //  velocidad avion

        MethodChecker mc = new MethodChecker();

        //////////////////////////////////////////////////////////////    Constructor   ///////////////////////////////////////////////////////////////////
        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;                                                                       //  Inicializamos el Identificador
            this.currentPosition = new Position(cpx, cpy);                                      //  Inicializamos la pos Actual
            this.initialPosition = new Position(cpx, cpy);                                      //  Inicializamos la pos Inicial
            this.finalPosition = new Position(fpx, fpy);                                        //  Inicializamos la pos Final
            this.velocidad = velocidad;                                                         //  Inicializamos la velocidad
        }
        ////////////////////////////////////////////////////////////////    Metodos GET   /////////////////////////////////////////////////////////////////////

        public string Getid()                                                                   //  Metodo Get para el Identificador
        { return this.id; }
        public Position GetinitialPosition()                                                    //  Metodo Get para la posicion Final
        { return this.initialPosition; }
        public Position GetcurrentPosition()                                                    //  Metodo Get para la posicion Actual
        { return this.currentPosition; }
        public Position GetfinalPosition()                                                      //  Metodo Get para la posicion Final
        { return this.finalPosition; }
        public double GetVelocidad()                                                            //  Metodo Get para la velocidad del avion
        { return this.velocidad; }

        ////////////////////////////////////////////////////////////////    Metodos SET   //////////////////////////////////////////////////////////////////////
        public void Setid(string id)                                                            //  Metodo Set para el Identificador del Avion 
        { this.id = id; }
        public void SetinitialPosition(Position iP)                                             //  Metodo Set para la posicion actual del avion
        { this.initialPosition = iP; }
        public void SetcurrentPosition(Position cP)                                             //  Metodo Set para la posicion actual del avion
        { this.currentPosition = cP; }
        public void SetfinalPosition(Position fP)                                               //  Metodo Set para la posicion final del avion
        { this.finalPosition = fP; }
        public void SetVelocidad(double velocidad)                                              //  Metodo Set para la variable velocidad
        { this.velocidad = velocidad; }

        ////////////////////////////////////////////////////////////    Metodos booleanos   /////////////////////////////////////////////////////////////////////
        public bool EstaDestino()                                                               //  Metodo para comporbar si el avion esta en Destion
        {
            bool resultado = false;                                                             //  El avion empieza sin estar en destino
            if (currentPosition == finalPosition) resultado = true;                              //  En caso q la pos actual = final, estamos en destino

            return resultado;                                                                   //  Devolvemos como un bool si hemos llegado o no
        }
        public bool Conflicto(FlightPlan b, double distanciaSeguridad)                          //  Metodo para comprobar si dos aviones estan en conflicto
        {
            bool conflicto = false;                                                             //  Inicialmente los aviones no estan en conflicto

            if (this.currentPosition.Distancia(b.currentPosition) < distanciaSeguridad)         //  Condicion de conflicto
                return true;                                                                    //  Si se cumple devolvemos que estamos en conflicto

            return conflicto;                                                                   //  Devolvemos nuestra situacion
        }
        public bool HasArrived()
        {
            if (EstaDestino()) return true;
            else return false;
        }
        ///////////////////////////////////////////////////////////     Metodos doubles   ///////////////////////////////////////////////////////////////////////
        public double Distance(FlightPlan plan) 
        {
            return plan.currentPosition.Distancia(plan.finalPosition);
        }
        ////////////////////////////////////////////////////////////     Metodos voids   ////////////////////////////////////////////////////////////////////////
        public void Mover(double tiempo)                                                        //  Metodo para mover los aviones
        {
            double distancia = tiempo * this.velocidad / 60;                                    //  Variable Double para calcular dist recorrida en x tiempo


            double hipotenusa =                                                                 //  Calculamos la hipotenusa
                Math.Sqrt(
                (finalPosition.GetX() - currentPosition.GetX())                                 //  Cordenada X
                * (finalPosition.GetX() - currentPosition.GetX())                               //  Elevamos al cuadrado
                + (finalPosition.GetY() - currentPosition.GetY())                               //  Cordenada Y
                * (finalPosition.GetY() - currentPosition.GetY())                              //  Elevamos al cuadrado
                );

            double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;       //  Calculamos el Coseno
            double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;         //  Calculamos el Seno
            //Caculamos la nueva posición del vuelo
            double x = currentPosition.GetX() + distancia * coseno;                             //  Calculamos la nueva cordX
            double y = currentPosition.GetY() + distancia * seno;                               //  Calculamos la nueva cordY

            Position nextPosition = new Position(x, y);                                         //  La nueva posicion calculada sera

            if (currentPosition.Distancia(nextPosition) < hipotenusa)                           //  En caso que la distancia a la nueva pos sea menor a la hipotenusa
                currentPosition = nextPosition;                                                 //  La posicion actual es la nueva pos.

            else currentPosition = finalPosition;                                               //  La posicion actual es la final

        }
        public void Restart()
        {
            SetinitialPosition(currentPosition);
        }
        public void EscribeConsola()                                                            //  Metodo para escribir en pantalla todos los datos
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Datos del vuelo: ");
            Console.WriteLine("Identificador: {0}", this.id);
            Console.WriteLine("Velocidad: {0:F2}", this.velocidad);
            Console.WriteLine("Posición actual: ({0:F2},{1:F2})", this.currentPosition.GetX(), this.currentPosition.GetY());
            if (this.EstaDestino()) 
            {
                Console.WriteLine("Ha llegado al destino");
                HasArrived();
                Restart();
                Console.WriteLine("Posicion Reiniciada");
            }            
            Console.WriteLine("******************************");
        }
    }
}
