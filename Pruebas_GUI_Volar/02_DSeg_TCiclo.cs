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
    public partial class DSeg_TCiclo : Form
    {
        Options menu;
        MethodChecker checker = new MethodChecker();
        public DSeg_TCiclo(Options menu)
        {
            InitializeComponent();
            this.menu = menu;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AvisosBox av;
            try 
            {
                double dseg = Convert.ToDouble(DistBox.Text);
                double tcicl = Convert.ToDouble(TimeBox.Text);

                if (checker.check_dseg(dseg) != "0")
                {
                    av = new AvisosBox(checker.check_dseg(dseg));
                    av.Show(this);
                }
                else if (checker.check_tcicl(tcicl) != "0") 
                {
                    av = new AvisosBox(checker.check_tcicl(tcicl));
                    av.Show(this);
                }
                else
                {
                    menu.dSeg = dseg;
                    menu.tCicl = tcicl;
                    av = new AvisosBox("Datos de simulacion Guardados!");
                    av.Show(this);
                }
            }
            catch (FormatException)
            {
                av = new AvisosBox("No se han introducido los datos con su formato correcto!");
                av.Show(this);
            }

        }
    }
}
