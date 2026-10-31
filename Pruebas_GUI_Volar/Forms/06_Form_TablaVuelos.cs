using System;
using System.Drawing;
using System.Windows.Forms;
using FlightLib;

namespace Pruebas_GUI_Volar.Forms
{
    public partial class Form_TablaVuelos : Form
    {
        // =========================================================================
        // 1. ATRIBUTS (Variables de la classe)
        // =========================================================================
        private FlightPlan v1; 
        private FlightPlan v2; 

        // =========================================================================
        // 2. CONSTRUCTOR
        // =========================================================================
        public Form_TablaVuelos(FlightPlan vuelo1, FlightPlan vuelo2)
        {
            InitializeComponent(); 

            this.v1 = vuelo1;
            this.v2 = vuelo2;

            CargarDatosEnTabla();
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

       
        private void Form_TablaVuelos_Load(object sender, EventArgs e)
        {
        }
        

        // =========================================================================
        // 3. MÈTODE PER CARREGAR LES DADES A LA TAULA
        // =========================================================================
        private void CargarDatosEnTabla()
        {
            //Configurem la taula 
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

      
            dataGridView1.Columns.Add("ID", "Identificador");
            dataGridView1.Columns.Add("PosActual", "Posición Actual");
            dataGridView1.Columns.Add("PosFinal", "Posición Final");
            dataGridView1.Columns.Add("Velocidad", "Velocidad");

           
            if (v1 != null)
            {
                Position posAct1 = v1.GetcurrentPosition();
                Position posFin1 = v1.GetfinalPosition();

                string textoPosActual1 = "(" + posAct1.GetX().ToString("F2") + ", " + posAct1.GetY().ToString("F2") + ")";
                string textoPosFinal1 = "(" + posFin1.GetX().ToString("F2") + ", " + posFin1.GetY().ToString("F2") + ")";

                // Afegim la fila del Vol 1
                dataGridView1.Rows.Add(v1.Getid(), textoPosActual1, textoPosFinal1, v1.GetVelocidad().ToString("F2"));
            }

            
            if (v2 != null)
            {
                Position posAct2 = v2.GetcurrentPosition();
                Position posFin2 = v2.GetfinalPosition();

                string textoPosActual2 = "(" + posAct2.GetX().ToString("F2") + ", " + posAct2.GetY().ToString("F2") + ")";
                string textoPosFinal2 = "(" + posFin2.GetX().ToString("F2") + ", " + posFin2.GetY().ToString("F2") + ")";

                // Afegim la fila del Vol 2
                dataGridView1.Rows.Add(v2.Getid(), textoPosActual2, textoPosFinal2, v2.GetVelocidad().ToString("F2"));
            }
        }
        // =========================================================================
        // 4. FUNCIO PER ENSENYAR DISTANCIA ENTRE VOLS
        // =========================================================================
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Comprovem que s'ha clicat una fila vàlida i no la capçalera
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                string idSeleccionat = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();

                FlightPlan seleccionat = null;
                FlightPlan altre = null;

                
                if (v1 != null && v1.Getid() == idSeleccionat)
                {
                    seleccionat = v1;
                    altre = v2;
                }
                else if (v2 != null && v2.Getid() == idSeleccionat)
                {
                    seleccionat = v2;
                    altre = v1;
                }

                // Si tenim tots dos vols, calculem la distància 
                if (seleccionat != null && altre != null)
                {
                    double distancia = seleccionat.GetcurrentPosition().Distancia(altre.GetcurrentPosition());

                    string missatge = string.Format(
                        "Vuelo seleccionado: {0}\n" +
                        "Otro vuelo: {1}\n\n" +
                        "Distancia actual entre ambos: {2:F2} unidades",
                        seleccionat.Getid(),
                        altre.Getid(),
                        distancia
                    );

                    // Popup
                    MessageBox.Show(missatge, "Distancia entre vuelos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Se necesitan dos vuelos para calcular la distancia.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}