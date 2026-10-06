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
    public partial class _03_Espacio_Aereo : Form
    {
        Options menu;
        FlightPlan AvionA;
        FlightPlan AvionB;
        public _03_Espacio_Aereo(Options menu)
        {
            InitializeComponent();
            this.menu = menu;
            this.AvionA = menu.vueloA;
            this.AvionB = menu.vueloB;

            if(AvionA == null) AvionABox.Visible = false;
            else AvionABox.Visible =true;

            if (AvionB == null) AvionBBox.Visible = false;
            else AvionBBox.Visible = true;
        }

        public void DibujarAvion(PictureBox AvionBox, Position pos)
        {
            AvionBox.Location = new Point((int)pos.GetX() - AvionBox.Width/2,
                                          (int)pos.GetY() - AvionBox.Width/2);
        }
        public void ColocarAvion(FlightPlan Avion, PictureBox AvionBox)
        {
            Avion.Restart();
            DibujarAvion(AvionBox, Avion.GetcurrentPosition());

        }
        
        public void MoverAvion(FlightPlan Avion, PictureBox AvionBox)
        {
            Avion.Mover(menu.tCicl);
            DibujarAvion(AvionBox, Avion.GetcurrentPosition());
        }
        private void LoadBut_Click(object sender, EventArgs e)
        {
            if(AvionA != null)
            {
                ColocarAvion(AvionA, AvionABox);
            }
            if (AvionB != null)
            {
                ColocarAvion(AvionB, AvionBBox);
            }
        }

        private void CicloBut_Click(object sender, EventArgs e)
        {
            if (AvionA != null)
            {
                MoverAvion(AvionA, AvionABox);
            }
            if (AvionB != null)
            {
                MoverAvion(AvionB, AvionBBox);
            }
        }
    }
}
