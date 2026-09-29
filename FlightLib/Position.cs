using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////    Posicion del Avion   /////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class Position
    {
        //////////////////////////////////////////////////////////////    Atributos   /////////////////////////////////////////////////////////////////////
        double x;                                                                   //  coordenada X (2D), formato double para mas precision
        double y;                                                                   //  coordenada Y (2D), formato double para mas precision

        //////////////////////////////////////////////////////////////    Constructor   ///////////////////////////////////////////////////////////////////
        public Position(double x, double y)                                         //  Creamos el constructor en funcion de variables x e y
        {
            this.x = x;                                                             //  La variable x se le asigna el input x
            this.y = y;                                                             //  La variable y se le asigna el input y
        }

        ///////////////////////////////////////////////////////////     Metodos doubles   /////////////////////////////////////////////////////////////////
        public double GetX(){ return x; }                                           //  Creamos el Get de la variable x

        public double GetY(){ return y; }                                           //  Creamos el Get de la variable y

        public double Distancia(Position b)                                         //  Creamos un metodo para calcular distancias, solicitando posicion final
        {
            double resultado =                                                      //  Creamos la variable resultado
                Math.Sqrt((x - b.x) * (x - b.x) + (y - b.y) * (y - b.y));           //  Esta calcula la distancia entre los dos puntos mediante pitagoras
            return resultado;                                                       //  Devolveos la distancia
        }
    }
}
