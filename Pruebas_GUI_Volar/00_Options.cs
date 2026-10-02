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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void datosDeVueloToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            Datos_de_Vuelo Dv = new Datos_de_Vuelo();
            Dv.Show();

        }

        private void datosDeVueloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos_de_Vuelo Dv = new Datos_de_Vuelo();
            Dv.Show(this);
        }

        private void dSegYTCicloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DSeg_TCiclo Dt = new DSeg_TCiclo();
            Dt.Show(this);
        }

        private void espacioAereoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _03_Espacio_Aereo Ea = new _03_Espacio_Aereo();
            Ea.Show(this);
        }
    }
}
