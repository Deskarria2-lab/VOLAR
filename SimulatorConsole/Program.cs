using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightLib; 

namespace SimulatorConsole
{
    public class Program
    {   
        static void Main(string[] args)
        {
            FlightPlanList planList = new FlightPlanList();
            try                                                                             //  Comprobar que va todo bien
            {
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////    DATOS AVION A   //////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine("Escribe el identificador");                              //  Solicitamos el identificador del avion
                string identificador = Console.ReadLine();                                  //  Guardamos el dato como un string

                Console.WriteLine(                                                          //  Solicitamos la velocidad del avion
                    "Escribe la velocidad del avion con identificador {0}"                  //  con el identificador guardado
                    , identificador
                    );

                                                                                            //  Guardamos la velocidad como un double
                double velocidad = Convert.ToDouble(Console.ReadLine());                    //  Porque podemos añadir muchos decimales, no
                                                                                            //  Sirve un floar, se puede quedar corto

                Console.WriteLine                                                           //  Pedimos al usuario que escriba la pos t_0
                    (                                                                       //  del avion 1 como un vector separado por un espacio
                    "Escribe las coordenadas de la posición inicial," +                     //  para poder separar los datos con split            
                    " separadas por un blanco del avion con identificador {0}",             //  y mandarlo a la libreria Position
                    identificador
                    );

                string linea = Console.ReadLine();                                          //  Guardamos la posicion inicial como string
                string[] trozos = linea.Split(' ');                                         //  Lo separamos segun la cantidad de espacios
                double ix = Convert.ToDouble(trozos[0]);                                    //  Definimos la cord x como la pos 0 del vector
                double iy = Convert.ToDouble(trozos[1]);                                    //  Definimos la cord y como la pos 1 del vector

                Console.WriteLine                                                           //  Pedimos al usuario que escriba la pos t_f
                    (                                                                       //  del avion 1 como un vector separado por un espacio
                    "Escribe las coordenadas de la posición final" +                        //  para poder separar los datos con split
                    ", separadas por un blanco del avion con identificador {0}",            //  y mandarlo a la libreria Position
                    identificador
                    );

                linea = Console.ReadLine();                                                 //  Guardamos la posicion inicial como string
                trozos = linea.Split(' ');                                                  //  Lo separamos segun la cantidad de espacios
                double fx = Convert.ToDouble(trozos[0]);                                    //  Definimos la cord x como la pos 0 del vector
                double fy = Convert.ToDouble(trozos[1]);                                    //  Definimos la cord y como la pos 1 del vector

                FlightPlan plan_a = new FlightPlan                                          //  Definimos el objeto con los atributos definidos
                    (identificador,                                                         //  Identificador de vuelo
                    ix,                                                                     //  Posicion inicial en el eje x
                    iy,                                                                     //  Posicion inicial en el eje y
                    fx,                                                                     //  Posicion final en el eje x
                    fy,                                                                     //  Posicion final en el eje y
                    velocidad);                                                             //  Velocidad de vuelo del avion

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////    DATOS AVION B   //////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine("Escribe el identificador");                              //  Solicitamos el identificador del avion
                identificador = Console.ReadLine();                                         //  Guardamos el dato como un string

                Console.WriteLine(                                                          //  Solicitamos la velocidad del avion
                    "Escribe la velocidad del avion con identificador {0}"                  //  con el identificador guardado
                    , identificador
                    );

                                                                                            //  Guardamos la velocidad como un double
                velocidad = Convert.ToDouble(Console.ReadLine());                           //  Porque podemos añadir muchos decimales, no
                                                                                            //  Sirve un floar, se puede quedar corto

                Console.WriteLine                                                           //  Pedimos al usuario que escriba la pos t_0
                    (                                                                       //  del avion 1 como un vector separado por un espacio
                    "Escribe las coordenadas de la posición inicial," +                     //  para poder separar los datos con split            
                    " separadas por un blanco del avion con identificador {0}",             //  y mandarlo a la libreria Position
                    identificador
                    );

                linea = Console.ReadLine();                                                 //  Guardamos la posicion inicial como string
                trozos = linea.Split(' ');                                                  //  Lo separamos segun la cantidad de espacios
                ix = Convert.ToDouble(trozos[0]);                                           //  Definimos la cord x como la pos 0 del vector
                iy = Convert.ToDouble(trozos[1]);                                           //  Definimos la cord y como la pos 1 del vector

                Console.WriteLine                                                           //  Pedimos al usuario que escriba la pos t_f
                    (                                                                       //  del avion 1 como un vector separado por un espacio
                    "Escribe las coordenadas de la posición final" +                        //  para poder separar los datos con split
                    ", separadas por un blanco del avion con identificador {0}",            //  y mandarlo a la libreria Position
                    identificador
                    );

                linea = Console.ReadLine();                                                 //  Guardamos la posicion inicial como string
                trozos = linea.Split(' ');                                                  //  Lo separamos segun la cantidad de espacios
                fx = Convert.ToDouble(trozos[0]);                                           //  Definimos la cord x como la pos 0 del vector
                fy = Convert.ToDouble(trozos[1]);                                           //  Definimos la cord y como la pos 1 del vector

                FlightPlan plan_b = new FlightPlan                                          //  Definimos el objeto con los atributos definidos
                    (identificador,                                                         //  Identificador de vuelo
                    ix,                                                                     //  Posicion inicial en el eje x
                    iy,                                                                     //  Posicion inicial en el eje y
                    fx,                                                                     //  Posicion final en el eje x
                    fy,                                                                     //  Posicion final en el eje y
                    velocidad);                                                             //  Velocidad de vuelo del avion

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////    IMPRIMIR DATOS VUELO   ////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                planList.AddFlightPlan(plan_a);
                planList.AddFlightPlan(plan_b);
                double dist;


                int i = 0;                                                                  //  Contador con valor inicial 0
                int ciclos = 20;                                                            //  Numero de ciclos 
                int distanciaSeguridad = 10;                                                //  Definimos la dist de seguridad entre aviones
                while(i<ciclos)                                                             //  Hacemos un bucle del tamaño de ciclos
                {
                    planList.Mover(10);                                                     //  Movemos el avion a 10 u de tiempo
                    dist = plan_a.Distance(plan_a);
                    planList.EscribeConsola();                                              //  Escribimos sus datos en consola
                    Console.WriteLine(dist);
                    if (planList.GetFlightPlan(0).Conflicto(plan_b, distanciaSeguridad))    //  Si los aviones rompen la dist de seguriadad
                        Console.WriteLine("Conflicto!");                                    //  Informamos del conflicto
                    i++;                                                                    //  Actualizamos el contador
                }
                

                Console.ReadLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Error de Formato");
                Console.ReadLine() ;
            }
        }
    }
}
