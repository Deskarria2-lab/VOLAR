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
        private FlightPlanList listaVuelos;

        // =========================================================================
        // 2. CONSTRUCTOR
        // =========================================================================
        public Form_TablaVuelos(FlightPlanList listaVuelos)
        {
            InitializeComponent();

            this.listaVuelos = listaVuelos;
            this.v1 = listaVuelos.GetFlightPlan(0);
            this.v2 = listaVuelos.GetFlightPlan(1);

            CargarDatosEnTabla();

            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }


        private void Form_TablaVuelos_Load(object sender, EventArgs e)
        {
        }


        // =========================================================================
        // 3. MÈTODE PER CARREGAR LES DADES A LA TAULA
        // =========================================================================
        private void CargarDatosEnTabla()
        {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < 10; i++)
            {
                FlightPlan vuelo =
                    listaVuelos.GetFlightPlan(i);

                if (vuelo != null)
                {
                    Position actual =
                        vuelo.GetcurrentPosition();

                    Position final =
                        vuelo.GetfinalPosition();

                    dataGridView1.Rows.Add(
                        vuelo.Getid(),"(" + actual.GetX().ToString("F2") +", " + actual.GetY().ToString("F2") + ")","(" + final.GetX().ToString("F2") +", " + final.GetY().ToString("F2") + ")",
                        vuelo.GetVelocidad().ToString("F2"));
                }
            }
        }
        // =========================================================================
        // 4. FUNCIO PER ENSENYAR DISTANCIA ENTRE VOLS
        // =========================================================================
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Comprovem que s'ha clicat una fila vàlida i no la capçalera
            if (e.ColumnIndex == 3)
            {
                return; // Ignorem clics a la columna de Velocidad
            }
            if (e.RowIndex >= 0 && e.RowIndex < ( dataGridView1.Rows.Count -1 ))
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
            else
            {
                MessageBox.Show("Fila no válida seleccionada");
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 3)
            {
                return;
            }

            object valorId =
                dataGridView1.Rows[e.RowIndex]
                .Cells["ID"]
                .Value;

            object valorVelocitat =
                dataGridView1.Rows[e.RowIndex]
                .Cells["Velocidad"]
                .Value;

            if (valorId == null || valorVelocitat == null)
            {
                return;
            }

            string idSeleccionat = valorId.ToString();

            double novaVelocitat;

            if (!double.TryParse(
                valorVelocitat.ToString(),
                out novaVelocitat))
            {
                MessageBox.Show(
                    "La velocitat no és vàlida.");

                return;
            }

            if (novaVelocitat <= 0)
            {
                MessageBox.Show(
                    "La velocitat ha de ser més gran que zero.");

                return;
            }

            for (int i = 0; i < 10; i++)
            {
                FlightPlan vuelo =
                    listaVuelos.GetFlightPlan(i);

                if (vuelo != null &&
                    vuelo.Getid() == idSeleccionat)
                {
                    vuelo.SetVelocidad(novaVelocitat);
                    break;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}