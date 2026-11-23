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

namespace GUI_VOLAR
{
    public partial class _03_Avisos : Form
    {
        List<FlightPlan> vuelosEnConflicto;
        ConflictDetector conflictDetector;

        List<FlightPlan> cambiados;
        double dSeg;
        public _03_Avisos(string mensaje, bool boton, List<FlightPlan> vuelosEnConflicto, double dSeg)
        {
            InitializeComponent();
            av_text.Text = mensaje;
            Frm_A_CambioVel_Button.Visible = boton;
            Frm_A_11S_Button.Visible = boton;
            this.vuelosEnConflicto = vuelosEnConflicto;
            this.dSeg = dSeg;

        }

        private void Frm_A_CambioVel_Button_Click(object sender, EventArgs e)
        {
            FrmDashboard dash = Owner as FrmDashboard;

            if (dash == null) return;

            List<FlightPlan> cambiados = dash.ResolverConflictos();

            av_text.Text = cambiados.Count == 0
                ? "No se ha podido cambiar ninguna velocidad."
                : "Velocidades modificadas: " + cambiados.Count;

            Frm_A_CambioVel_Button.Visible = false;
            Frm_A_11S_Button.Visible = false;
        }

        private void Frm_A_11S_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
