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
        Options menu;
        MethodChecker checker = new MethodChecker();
        public Datos_de_Vuelo(Options menu)
        {
            InitializeComponent();
            cont = 0;
            this.menu = menu;
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
        private void saveButton_Click(object sender, EventArgs e)
        {
            AvisosBox av;

            try 
            {
                double vel = Convert.ToDouble(VelBox.Text);
                double xo = Convert.ToDouble(XoBox.Text);
                double yo = Convert.ToDouble(YoBox.Text);
                double xf = Convert.ToDouble(XfBox.Text);
                double yf = Convert.ToDouble(YfBox.Text);
                if (checker.check_id(IdBox.Text) != "0") 
                {
                    av = new AvisosBox(checker.check_id(IdBox.Text));
                    av.Show(this);
                }
                else if (checker.check_vel(vel) != "0")
                {
                    av = new AvisosBox(checker.check_vel(vel));
                    av.Show(this);
                }
                else if (checker.check_pos(xo, yo, xf, yf) != "0")
                {
                    av = new AvisosBox(checker.check_pos(xo, yo, xf, yf));
                    av.Show(this);
                }
                else 
                {
                    FlightPlan plan = new FlightPlan
                    (IdBox.Text,
                    vel,
                    xo,
                    yo,
                    xf,
                    yf);
                    if (cont == 1)
                    {
                        menu.vueloA = plan;
                        av = new AvisosBox("El Vuelo A esta creado!");
                        av.Show(this);
                    }
                    else if (cont == 2)
                    {
                        menu.vueloB = plan;
                        av = new AvisosBox("El Vuelo B esta creado!");
                        av.Show(this);
                    }
                }
              
            }catch (FormatException)
            {
                av = new AvisosBox("No se han introducido los datos con su formato correcto!");
                av.Show(this);
            }
        }
    }
}
