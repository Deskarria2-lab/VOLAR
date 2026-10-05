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
    /////////////////////////////////////////////////    Clase para gestionar el menu de Opciones   ////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class Options : Form
    {
        FlightPlanList planList = new FlightPlanList();
        public FlightPlan vueloA;
        public FlightPlan vueloB;
        public double dSeg;
        public double tCicl;
    //////////////////////////////////////////////////////////////    Constructor   ///////////////////////////////////////////////////////////////////
        public Options()
        {
            InitializeComponent();
            planList.AddFlightPlan(vueloA);
            planList.AddFlightPlan(vueloB);
        }
    ////////////////////////////////////////////////////////////     Metodos voids   ////////////////////////////////////////////////////////////////////////

        private void datosDeVueloToolStripMenuItem_Click(object sender, EventArgs e)                            //  Al Realizar Click en el boton Datos de vuelo
        {
            Datos_de_Vuelo Dv = new Datos_de_Vuelo(this);                                                           //  Creamos el objeto Datos de vuelo
            Dv.Show(this);                                                                                      //  Abrimos el Formulario Datos de Vuelo
        }

        private void dSegYTCicloToolStripMenuItem_Click(object sender, EventArgs e)                            //  Al Realizar Click en el boton definir ciclos
        {
            DSeg_TCiclo Dt = new DSeg_TCiclo(this);                                                             //  Creamos el objeto para definir ciclos
            Dt.Show(this);                                                                                      //  Abrimos el Formulario definir ciclos
        }

        private void espacioAereoToolStripMenuItem_Click(object sender, EventArgs e)                            //  Al Realizar Click en el boton Espacio Aereo
        {
            _03_Espacio_Aereo Ea = new _03_Espacio_Aereo(this);                                                 //  Creamos el objeto Espacio Aereo
            Ea.Show(this);                                                                                      //  Abrimos el Formulario Espacio Aereo
        }
    }
}
