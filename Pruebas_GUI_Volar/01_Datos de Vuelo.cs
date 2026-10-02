using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;

namespace Pruebas_GUI_Volar
{
    public partial class Datos_de_Vuelo : Form
    {
        int cont;
        public Datos_de_Vuelo()
        {
            InitializeComponent();
            cont = 0;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Vuelo A")
            {
                tituloVuelos.Text = "DATOS VUELO A:";
                cont = 1;
            }
            else if (e.Node.Text == "Vuelo B")
            {
                tituloVuelos.Text = "DATOS VUELO B:";
                cont = 2;
            }
            else cont = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (cont == 1)
            {
                FlightPlan vueloA = new FlightPlan
                    (
                    IdBox.Text,
                    Convert.ToDouble(VelBox.Text),
                    Convert.ToDouble(XoBox.Text),
                    Convert.ToDouble(YoBox.Text),
                    Convert.ToDouble(XfBox.Text),
                    Convert.ToDouble(YfBox.Text)
                    );
                tituloVuelos.Text = vueloA.GetVelocidad().ToString();
            }
            else if (cont == 2)
            {
                FlightPlan vueloB = new FlightPlan
                    (
                    IdBox.Text,
                    Convert.ToDouble(VelBox.Text),
                    Convert.ToDouble(XoBox.Text),
                    Convert.ToDouble(YoBox.Text),
                    Convert.ToDouble(XfBox.Text),
                    Convert.ToDouble(YfBox.Text)
                    );
                tituloVuelos.Text = vueloB.Getid();
            }
        }
    }
}
