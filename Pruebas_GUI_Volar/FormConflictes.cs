using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pruebas_GUI_Volar
{
    public partial class finestraConflictes : Form
    {
        private FlightPlanList llistaVols;
        private double distanciaSeguretat;

        public finestraConflictes()
        {
            InitializeComponent();
        }

        private void FormConflictes_Load(object sender, EventArgs e)
        {
            LabelConflicte.Text = "S'ha detectat conflicte entre dos avions. Dessitga arrelar-lo?";
        }

        private void solucionarConflicte_Click(object sender, EventArgs e)
        {
            // Creem el detector amb les dades que tenim guardades als atributs
            ConflictDetector detector = new ConflictDetector(llistaVols, distanciaSeguretat);

            // Executem el mètode que modifica les velocitats per evitar els xocs
            detector.ResolverConflictos();

            // Informem a l'usuari i tanquem la finestra
            MessageBox.Show("S'han aplicat canvis de velocitat per resoldre els conflictes.");
            this.Close();


        }

        private void noSolucionarConflicte_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
