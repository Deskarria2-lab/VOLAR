using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////    Lista de Aviones   ///////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class FlightPlanList
    {
        //////////////////////////////////////////////////////////////    Atributos   ////////////////////////////////////////////////////////////////////
        FlightPlan[] PlaneList;                                                             //  Creamos un vector de objetos llamado PlaneList
        int n_planes;                                                                       //  Creamos un contador de aviones dentro de la Lista
        int v_lenght;

        //////////////////////////////////////////////////////////////    Constructor   ///////////////////////////////////////////////////////////////////
        public FlightPlanList()                                                             //  Creamos el constructor de la clase
        {
            this.v_lenght = 10;
            this.PlaneList = new FlightPlan[v_lenght];                                      //  El vector tiene una capacidad de v_lenght aviones
            this.n_planes = 0;                                                              //  El numero inicial de aviones almacenados es 0
        }

        //////////////////////////////////////////////////////////     Metodos Integers   ////////////////////////////////////////////////////////////////////////
        public int AddFlightPlan(FlightPlan p)                                              //  Creamos la funcion añadari aviones al plan de vuelo       
        {
            if (this.n_planes >= this.v_lenght) return -1;                                  //  Nos aseguramos que la cantidad de aviones añadidos sea inferior al tope maximo
            else                                                                            //  Si estamos debajo del tope
            {
                PlaneList[n_planes] = p;                                                    //  Añadimos el avion al vector
                n_planes++;                                                                 //  Incrementamos la cantidad de aviones en la lista
                return 0;                                                                   //  Acabamos con todo en orden
            }
        }
        /////////////////////////////////////////////////////////     Metodos FlightPlan   ////////////////////////////////////////////////////////////////////////
        public FlightPlan GetFlightPlan(int number)                                         //  Creamos el Get de los datos del vector
        {
            if(number < 0 || number >= this.n_planes) return null;                          //  Nos aseguramos que los datos introducidos son validos
            else return PlaneList[number];                                                  //  En caso favorable devolvemos el dato solicitado
        }

        ////////////////////////////////////////////////////////////     Metodos voids   ////////////////////////////////////////////////////////////////////////
        public void Mover(double tiempo)                                                    //  Creamos el metodo Mover avion 
        {
            for (int i = 0; i < this.n_planes; i++) PlaneList[i].Mover(tiempo);             //  Movemos todos los aviones en la lista
        }
        public void EscribeConsola()                                                        //  Creamos el metodo escribir en consola
        {
            for (int i = 0; i < this.n_planes; i++) PlaneList[i].EscribeConsola();          //  Escribimos los datos de todos los aviones
        }
    }
}
