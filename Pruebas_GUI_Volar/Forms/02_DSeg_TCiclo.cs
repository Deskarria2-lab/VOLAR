using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pruebas_GUI_Volar
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////    Clase para gestionar el menu de Datos de Simu   /////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class DSeg_TCiclo : Form
    {
        //////////////////////////////////////////////////////////    Atributos   ////////////////////////////////////////////////////////////////////
        Options menu;                                                                   //  Creamos el objeto Options con nombre menu
        MethodChecker checker = new MethodChecker();                                    //  Creamos un objeto MethodChecker
        AvisosBox av;                                                                   //  Creamos el objeto AvisoBox con nombre av
        double dseg;                                                                    //  Creamos la variable double distancia de seguridad
        double tcicl;                                                                   //  Creamos la variable double tiempos de ciclo

        ///////////////////////////////////////////////////////////     Constructor     ///////////////////////////////////////////////////
        public DSeg_TCiclo(Options menu)                                                //
        {                                                                               //
            InitializeComponent();                                                      //  Iniciamos los componentes, esto siempre esta;
            this.menu = menu;                                                           //  Inicializamos el menu;
        }                                                                               //
                                                                                        //
        ////////////////////////////////////////////////////////////     Metodos voids  /////////////////////////////////////////////////////////////////////////}
        private void button1_Click(object sender, EventArgs e)                          //  Metodo para guardar datos al pulsar el boton guardar
        {                                                                               //
            try                                                                         //  En caso que no haya ningun error de formato
            {                                                                           //
                dseg = Convert.ToDouble(DistBox.Text);                                  //  Guardamos los datos introducidos en DistBox
                tcicl = Convert.ToDouble(TimeBox.Text);                                 //  Guardamos los datos introducidos en TimeBox
                                                                                        //
                if (checker.check_dseg(dseg) != "0")                                    //  En caso que no tengamos un okey con el checker dist seguridad
                {                                                                       //
                    av = new AvisosBox(checker.check_dseg(dseg));                       //  Crea la ventana de avisos con los datos del checker
                    av.Show(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else if (checker.check_tcicl(tcicl) != "0")                             //  En caso que no tengamos un okey con el checker tiempo de ciclo
                {                                                                       //
                    av = new AvisosBox(checker.check_tcicl(tcicl));                     //  Crea la ventana de avisos con los datos del checker
                    av.Show(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else                                                                    //  Si todo esta bien
                {                                                                       //
                    menu.dSeg = dseg;                                                   //  Guardamos los datos dseg del menu con los datos introducidos
                    menu.tCicl = tcicl;                                                 //  Guardamos los datos tiempo de ciclo del menu con los datos introducidos
                    av = new AvisosBox("Datos de simulacion Guardados!");               //  Crea la ventana de avisos indicando que los datos para la simulacion estan bien guardados
                    av.Show(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
            }                                                                           //
            catch (FormatException)                                                     //  En caso de error de formato
            {                                                                           //
                av = new AvisosBox(                                                     //  Crea la ventana de avisos indicando
                    "No se han introducido los datos con su formato correcto!");        //  Que algun dato introducido no cumple formato
                av.Show(this);                                                          //  Muestra en pantalla
            }                                                                           //
        }
    }
}
