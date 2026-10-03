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
    public partial class DSeg_TCiclo : Form
    {
        Options menu;
        public DSeg_TCiclo(Options menu)
        {
            InitializeComponent();
            this.menu = menu;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menu.dSeg = Convert.ToDouble(DistBox.Text);
            menu.tCicl = Convert.ToDouble(TimeBox.Text);
            Titulo_Ajustes.Text = "Guardado!";
        }
    }
}
