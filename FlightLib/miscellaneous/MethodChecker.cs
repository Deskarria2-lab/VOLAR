using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////   Clase para gestionar los datos introducidos por el user /////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class MethodChecker
    {
        //////////////////////////////////////////////////////////////    Atributos   ////////////////////////////////////////////////////////////////////
       datas database = new datas();

        //////////////////////////////////////////////////////////    Metodos string    //////////////////////////////////////////////////////////////////
        public string check_id(string id)                                                                               //  Metodo para comprobar el Identificador del Avion
        {
            if (id == "")                                                                                               //  En caso que el usuario no introduzca datos en ID
                return "No se ha introducido un identificador";                                                         //  Devolvemmos el error
            if (id.Length != 6)                                                                                         //  Si el ID no tiene la longitud establecida
                return "El Identificador debe tener una longitud de 6 caracteres," +                                    //  Devolvemos una explicacion breve de como debe ser el 
                    " un cabezal y una codificacion numerica";                                                          //  Identificador de vuelo

            string head_id = id.Substring(0, 2);                                                                        //  Cojemos los dos primero datos del ID (Header)
            head_id = head_id.ToUpper();                                                                                //  Nos aseguramos que el ID esta en majusculas
            if (!database.codecheck(head_id))                                                                           //  Si el ID no existe en data.cs
                return "El header del ID no es valido, no esta dentro de la base de datos";                             //  Devolvemos el error explicando que el Header no existe

            else                                                                                                        //  Si todo esta bien
                return "0";                                                                                             //  Devuelve el string "0"
        }
        public string check_vel(double vel)                                                                             //  Metodo para comprobar la velocidad del avion
        {
            if (vel < 0) return "La velocidad no puede ser negativa";                                                   //  Si el usuario consigue introducir vel negativa, error
            else if (vel == 0) return "La velocidad no puede ser nula";                                                 //  Si el usuario introduce una velocidad 0, error
            else return "0";                                                                                            //  Si todo esta bien devuelve el string "0"
        }
        public string check_pos(double xo, double yo, double xf, double yf)                                             //  Metodo para introducir las cordenadas del avion
        {
            if (xo == xf && yo == yf)                                                                                   //  Si la posicion inicial y final son iguales
                return "La posicion inicial y final no deben ser la misma, no hay vuelo!";                              //  Devolver error, no hay desplazamiento posible

            else if (xo < 0) return "La cord x0 no debe ser negativa!";                                                 //  Error en xo si la cordenada es negativa
            else if (yo < 0) return "La cord y0 no debe ser negativa!";                                                 //  Error en yo si la cordenada es negativa
            else if (xf < 0) return "La cord xf no debe ser negativa!";                                                 //  Error en xf si la cordenada es negativa
            else if (yf < 0) return "La cord yf no debe ser negativa!";                                                 //  Error en yf si la cordenada es negativa

            else if (xo > 599) return "La cord xo esta fuera de rango! el limite es 599)";                              //  Error en xo si salimos de rango simulacion
            else if (yo > 599) return "La cord yo esta fuera de rango! el limite es 599)";                              //  Error en yo si salimos de rango simulacion;
            else if (xf > 599) return "La cord xf esta fuera de rango! el limite es 599)";                              //  Error en xf si salimos de rango simulacion;
            else if (yf > 599) return "La cord yf esta fuera de rango! el limite es 599)";                              //  Error en yf si salimos de rango simulacion;

            else return "0";                                                                                            //  Si todo esta bien devuelve el string "0";
        }

        public string check_dseg(double dseg)                                                                           //  Metodo para comprobar la distancia de Seguridad
        {
            if (dseg < 0) return "La distancia de seguridad no puede ser menor a 0!";                                   //  Error si se introduce distancia negativa
            else if (dseg == 0) return "La distancia de seguridad no puede ser  0!";                                    //  Error si se introduce distancia 0
            else return "0";                                                                                            //  Si todo esta bien devuelve el string "0";
        }
        public string check_tcicl(double tcicl)                                                                         //  Metodo para comprobar el tiempo de ciclo de simu
        {
            if (tcicl < 0) return "El tiempo no puede ser menor a 0!";                                                  //  Error si el tiempò es negativo
            else if (tcicl == 0) return "El tiempo no puede ser 0!";                                                    //  Error si el tiempo es 0
            else return "0";                                                                                            //  Si todo esta bien devuelve el string "0";
        }
    }
}
