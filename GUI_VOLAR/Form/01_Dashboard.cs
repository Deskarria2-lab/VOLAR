using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace GUI_VOLAR
{
    public partial class FrmDashboard : Form
    {
        FrmLogin login;
        public FlightPlanList planList;
        public FlightPlan vueloA;
        public FlightPlan vueloB;
        public FlightPlan vueloSelecionado;
        _03_Avisos av;

        ////////////////////////////////////////////////////// Atributos de Configuracion /////////////////////////////////////////////////////
        public double dSeg;
        public double tCicl;
        int cont;
        MethodChecker checker;
        GesitonFicheros reader;
        ////////////////////////////////////////////////////// Atributos Datos de Vuelo //////////////////////////////////////////////////////
        string id;
        double vel;
        double xo;
        double yo;
        double xf;
        double yf;
        ////////////////////////////////////////////////////// Atributos Simulacion /////////////////////////////////////////////////////////
        private readonly List<FlightPlan> volsCarregats;
        Timer reloj = new Timer { Interval = 33 };
        readonly Stack<Position[]> positions;
        bool aviones_cargados;
        bool conf_cargada;
        bool datos_cargados;
        ConflictDetector conflictDetector;
        public FrmDashboard(FrmLogin login)
        {
            InitializeComponent();
            ConfigurarGrid();
            positions = new Stack<Position[]>();
            reader = new GesitonFicheros();
            this.planList = new FlightPlanList();
            this.checker = new MethodChecker();
            this.login = login;
            this.volsCarregats = new List<FlightPlan>();
            cont = 1;
            Frm_Editor_Aviones.Visible = true;
            Frm_Radar_Panel.VueloSeleccionado += Radar_VueloSeleccionado;
            Frm_DataGrid.CellEndEdit += Frm_DataGrid_CellEndEdit;
            Frm_Dist_Text.Text = "50";
            Frm_Time_Text.Text = "5";
            reloj.Tick += Reloj_Tick;

            aviones_cargados = false;
            conf_cargada = false;
            datos_cargados = false;

            Frm_S_Cargar_Button.Enabled = false;
            Frm_S_Start_Button.Enabled = false;
            Frm_S_UnCiclo.Enabled = false;
            Frm_S_Restart_Button.Enabled = false;
            Frm_S_Stop_Button.Enabled = false;
            Frm_S_PasoAnterior.Enabled = false;
            Frm_S_Download_Button.Enabled = false;
        }

        private void FrmExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Frm_Editor_Button_Click(object sender, EventArgs e)
        {
            Frm_Editor_Aviones.Visible = true;
        }

        private void Frm_Config_Button_Click(object sender, EventArgs e)
        {
            //AQUI SE MOSTRARAN DATOS DEL USUARIO EN UN FUTURO Y SE PERMITIRA CONFIGURAR EN PROFUNDIDAD EL PROGRAMA
        }

        private void Frm_Help_Click(object sender, EventArgs e)
        {
            //AQUI SE MOSTRARA UNA GUIA AL USUARIO EN DOCUMENTO PDF
        }
        private void Frm_Conf_AvionA_Button_Click(object sender, EventArgs e)
        {
            cont = 1;
            Frm_Editor_Aviones.Visible = true;
            Frm_Avion_Label.Text = "Avion A:";
            Frm_ID_Text.Text = null;
            Frm_Vel_Text.Text = null;
            Frm_X0_Text.Text = null;
            Frm_Y0_Text.Text = null;
            Frm_Xf_Text.Text = null;
            Frm_Yf_Text.Text = null;

        }

        private void Frm_Conf_AvionB_Button_Click(object sender, EventArgs e)
        {
            cont = 2;
            Frm_Editor_Aviones.Visible = true;
            Frm_Avion_Label.Text = "Avion B:";
            Frm_ID_Text.Text = null;
            Frm_Vel_Text.Text = null;
            Frm_X0_Text.Text = null;
            Frm_Y0_Text.Text = null;
            Frm_Xf_Text.Text = null;
            Frm_Yf_Text.Text = null;
        }

        private void Frm_Conf_Save_Button_Click(object sender, EventArgs e)
        {
            try                                                                         //  En caso que no haya ningun error de formato
            {
                id = Frm_ID_Text.Text;                                                        //  La variable ID tiene los datos introducidos en IdBox
                vel = Convert.ToDouble(Frm_Vel_Text.Text);                                    //  La variable vel tiene los datos introducidos en VelBox
                xo = Convert.ToDouble(Frm_X0_Text.Text);                                      //  La variable xo tiene los datos introducidos en XoBox
                yo = Convert.ToDouble(Frm_Y0_Text.Text);                                      //  La variable yo tiene los datos introducidos en YoBox;
                xf = Convert.ToDouble(Frm_Xf_Text.Text);                                      //  La variable xf tiene los datos introducidos en XfBox;
                yf = Convert.ToDouble(Frm_Yf_Text.Text);                                      //  La variable yf tiene los datos introducidos en YfBox;

                if (checker.check_id(id) != "0")                                        //  Si el checker del Id no devuelve un okey
                {                                                                       //
                    av = new _03_Avisos(checker.check_id(id));                           //  Crea la ventana de avisos con los datos del checker
                    av.ShowDialog(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else if (checker.check_vel(vel) != "0")                                 //  Si el checker de la velocidad no devuelve un okey
                {                                                                       //
                    av = new _03_Avisos(checker.check_vel(vel));                        //  Crea la ventana de avisos con los datos del checker;
                    av.ShowDialog(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else if (checker.check_pos(xo, yo, xf, yf) != "0")                      //  Si el checker de las posiciones no devuelve un okey
                {                                                                       //
                    av = new _03_Avisos(checker.check_pos(xo, yo, xf, yf));             //  Crea la ventana de avisos con los datos del checker
                    av.ShowDialog(this);                                                //  Abre la ventana de avisos
                }                                                                       //
                else                                                                    //  Si todo esta en orden
                {                                                                       //  
                    if (cont == 1)                                                      //  Si el contador esta en 1
                    {                                                                   //
                        if (vueloA != null)
                        {
                            vueloA.ActualizarVuelo(vueloA, id,
                            vel,
                            xo,
                            yo,
                            xf,
                            yf);
                        }
                        vueloA = new FlightPlan                                    //  Le damos los datos pertinentes al vueloA
                            (id,                                                        //  Su ID
                            xo,                                                         //  Su Vel
                            yo,                                                         //  Su xo
                            xf,                                                         //  Su yo
                            yf,                                                         //  Su xf
                            vel);                                                       //  Su yf
                        planList.AddFlightPlan(vueloA);
                        av = new _03_Avisos("El Vuelo A esta creado!");                  //  Crea la ventana de avisando del vuelo creado
                        av.ShowDialog(this);                                                  //  Abre la ventana de avisos
                        aviones_cargados = true;
                    }                                                                   //
                    else if (cont == 2)                                                 //  Si el contador esta en 2
                    {
                        if (vueloB != null)
                        {
                            vueloB.ActualizarVuelo(vueloB, id,
                            vel,
                            xo,
                            yo,
                            xf,
                            yf);
                        }
                        vueloB = new FlightPlan                                    //  Le damos los datos pertinentes al vueloA
                            (id,                                                        //  Su ID
                            xo,                                                         //  Su Vel
                            yo,                                                         //  Su xo
                            xf,                                                         //  Su yo
                            yf,                                                         //  Su xf
                            vel);                                                       //  Su yf
                        planList.AddFlightPlan(vueloB);
                        av = new _03_Avisos("El Vuelo B esta creado!");                  //  Crea la ventana de avisando del vuelo creado
                        av.ShowDialog(this);                                                  //  Abre la ventana de avisos
                        aviones_cargados = true;
                    }
                    if (conf_cargada && aviones_cargados)
                    {
                        Frm_S_Cargar_Button.Enabled = true;
                        Frm_S_Start_Button.Enabled = true;
                        Frm_S_UnCiclo.Enabled = true;
                        Frm_S_Restart_Button.Enabled = true;
                        Frm_S_Stop_Button.Enabled = true;
                        Frm_S_PasoAnterior.Enabled = true;
                        Frm_S_Download_Button.Enabled = true;
                    }
                }                                                                       //
            }
            catch (FormatException)                                                    //  En Caso de error de formato
            {
                av = new _03_Avisos(                                                    //  Crea la ventana avisando
                    "No se han introducido los datos con su formato correcto!");        //  Que los datos introducidos no son correctos
                av.ShowDialog(this);                                                          //  Muestra esta ventana//
            }
        }

        private void Frm_Conf_Save_DS_Button_Click(object sender, EventArgs e)
        {
            try 
            {
                dSeg = Convert.ToDouble(Frm_Dist_Text.Text);                                  //  Guardamos los datos introducidos en DistBox
                tCicl = Convert.ToDouble(Frm_Time_Text.Text);                                 //  Guardamos los datos introducidos en TimeBox
                                                                                              //
                if (checker.check_dseg(dSeg) != "0")                                    //  En caso que no tengamos un okey con el checker dist seguridad
                {                                                                       //
                    av = new _03_Avisos(checker.check_dseg(dSeg));                       //  Crea la ventana de avisos con los datos del checker
                    av.ShowDialog(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else if (checker.check_tcicl(tCicl) != "0")                             //  En caso que no tengamos un okey con el checker tiempo de ciclo
                {                                                                       //
                    av = new _03_Avisos(checker.check_tcicl(tCicl));                     //  Crea la ventana de avisos con los datos del checker
                    av.ShowDialog(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else                                                                    //  Si todo esta bien
                {                                                                       //
                    av = new _03_Avisos("Datos de simulacion Guardados!");              //  Crea la ventana de avisos indicando que los datos para la simulacion estan bien guardados
                    av.ShowDialog(this);                                                      //  Abre la ventana de avisos
                    conf_cargada = true;
                    Frm_Radar_Panel.DistanciaSeguridad = dSeg;
                }
                if(conf_cargada && aviones_cargados) 
                {
                    Frm_S_Cargar_Button.Enabled = true;
                }
            }
            catch 
            {
                av = new _03_Avisos(                                                    //  Crea la ventana avisando
                    "No se han introducido los datos con su formato correcto!");        //  Que los datos introducidos no son correctos
                av.ShowDialog(this);
            }

        }

        private void Frm_Conf_Auto_Button_Click(object sender, EventArgs e)
        {
            if(cont == 1) 
            {
                Frm_ID_Text.Text = "AA1234";
                Frm_Vel_Text.Text = "50";
                Frm_X0_Text.Text = "0";
                Frm_Y0_Text.Text = "0";
                Frm_Xf_Text.Text = "590";
                Frm_Yf_Text.Text = "590";
            }
            if(cont == 2) 
            {
                Frm_ID_Text.Text = "AA4567";
                Frm_Vel_Text.Text = "50";
                Frm_X0_Text.Text = "590";
                Frm_Y0_Text.Text = "0";
                Frm_Xf_Text.Text = "0";
                Frm_Yf_Text.Text = "590";
            }
        }

        private void Frm_Conf_File_Button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog selector = new OpenFileDialog())
            {
                selector.Title = "Selecciona el fitxer de vols";
                selector.Filter =
                    "Fitxers de text (*.txt)|*.txt|" +
                    "Fitxers CSV (*.csv)|*.csv|" +
                    "Tots els fitxers (*.*)|*.*";

                if (selector.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                reader.GestionarFicheros(selector.FileName);
                planList = reader.CargarVuelosDesdeFichero();
                av = new _03_Avisos(reader.EstadoFichero());
                aviones_cargados = true;
                if (conf_cargada && aviones_cargados)
                {
                    Frm_S_Cargar_Button.Enabled = true;
                    Frm_S_Start_Button.Enabled = true;
                    Frm_S_UnCiclo.Enabled = true;
                    Frm_S_Restart_Button.Enabled = true;
                    Frm_S_Stop_Button.Enabled = true;
                    Frm_S_PasoAnterior.Enabled = true;
                    Frm_S_Download_Button.Enabled = true;
                }
                av.ShowDialog(this);
            }
        }
        ///////////////////////////////////////////// Simulation Control, funciones para dibujar aviones /////////////////////////////////////
        
        public void GuardarPosiciones() 
        {
            Position[] foto = new Position[volsCarregats.Count];
            for (int i = 0; i < foto.Length; i++) 
            {
                foto[i] = volsCarregats[i].GetcurrentPosition();
            }
            positions.Push(foto);
        }

        public void DeshacerPaso() 
        {
            if (positions.Count == 0) return;

            reloj.Stop();
            Position[] foto = positions.Pop();
            for (int i = 0; i < foto.Length && i < volsCarregats.Count; i++) 
            {
                volsCarregats[i].SetcurrentPosition(foto[i]);
            }
            Frm_Radar_Panel.Invalidate();
            ActualizarGrid();
        }
        public void AvanzarCiclo() 
        {
            bool mov = false;
            foreach (FlightPlan plan in volsCarregats) 
            {
                if (!plan.HasArrived()){ mov = true; break; }
            }

            if (!mov) return;
            GuardarPosiciones();
            foreach (FlightPlan plan in volsCarregats)
            {
                plan.Mover(tCicl);
            }
            Frm_Radar_Panel.Invalidate();
            ActualizarGrid();
        }
        private void Reloj_Tick(object sender, EventArgs e) 
        {
            AvanzarCiclo();
        }
        private void Frm_S_Cargar_Button_Click(object sender, EventArgs e)
        {
            Frm_Radar_Panel.Vuelos.Clear();
            volsCarregats.Clear();
            positions.Clear();
            for (int i = 0; i < 10; i++) 
            {
                FlightPlan plan = planList.GetFlightPlan(i);
                if (planList.GetFlightPlan(i) != null) 
                {
                    plan.Restart();
                    volsCarregats.Add(plan);
                    Frm_Radar_Panel.Vuelos.Add(plan);
                }
            }
            conflictDetector = new ConflictDetector(planList, dSeg);
            Frm_Radar_Panel.Invalidate();
            GuardarPosiciones();
            CargarGrid();
            datos_cargados = true;
            if (datos_cargados) 
            {
                Frm_S_Start_Button.Enabled = true;
                Frm_S_UnCiclo.Enabled = true;
                Frm_S_Restart_Button.Enabled = true;
                Frm_S_Stop_Button.Enabled = true;
                Frm_S_PasoAnterior.Enabled = true;
                Frm_S_Download_Button.Enabled = true;
            }
        }

        private void Frm_S_UnCiclo_Click(object sender, EventArgs e)
        {
            AvanzarCiclo();
        }

        private void Frm_S_Start_Button_Click(object sender, EventArgs e)
        {
            reloj.Start();
        }

        private void Frm_S_Stop_Button_Click(object sender, EventArgs e)
        {
            reloj.Stop();
        }

        private void Frm_S_Restart_Button_Click(object sender, EventArgs e)
        {
            reloj.Stop();
            positions.Clear();

            foreach (FlightPlan plan in volsCarregats)
            {
                plan.Restart();
                plan.SetVelocidad(plan.GetVelocidadInicial());

            }

            Frm_Radar_Panel.Invalidate();
            ActualizarGrid();
        }

        private void Frm_S_PasoAnterior_Click(object sender, EventArgs e)
        {
            DeshacerPaso();
        }

        private void Radar_VueloSeleccionado(object sender, FlightPlan plan) 
        {
            vueloSelecionado = plan;
            SeleccionarEnGrid(plan);
        }


        ///////////////////////////////////////////// DATA GRID ////////////////////////////////////
        public void ConfigurarGrid() 
        {
            Frm_DataGrid.Columns.Clear();
            Frm_DataGrid.Columns.Add("ColId", "ID");
            Frm_DataGrid.Columns.Add("ColIni", "Pos. Inicial");
            Frm_DataGrid.Columns.Add("ColFin", "Pos. Final");
            Frm_DataGrid.Columns.Add("ColAct", "Pos. Actual");
            Frm_DataGrid.Columns.Add("colVel", "Velocidad");

            foreach (DataGridViewColumn columna in Frm_DataGrid.Columns)
            {
                columna.ReadOnly = true;
            }

            // Solo velocidad editable
            Frm_DataGrid.Columns["colVel"].ReadOnly = false;
            Frm_DataGrid.AllowUserToAddRows = false;
            Frm_DataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Frm_DataGrid.MultiSelect = false;
            Frm_DataGrid.DefaultCellStyle.SelectionBackColor = Color.Orange;
            Frm_DataGrid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(10, 20, 40);
            Frm_DataGrid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.Orange;
            Frm_DataGrid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(10, 20, 40);

            typeof(DataGridView).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(Frm_DataGrid, true, null);
        }

        public void CargarGrid() 
        {
            Frm_DataGrid.Columns.Clear();
            if(Frm_DataGrid.Columns.Count == 0 ) ConfigurarGrid();
            foreach (FlightPlan plan in volsCarregats) 
            {
                int n = Frm_DataGrid.Rows.Add();
                DataGridViewRow fila = Frm_DataGrid.Rows[n];
                fila.Tag = plan;                                   // la fila recuerda de qué avión es

                Position ini = plan.GetinitialPosition(), fin = plan.GetfinalPosition();
                fila.Cells["colId"].Value = plan.Getid();
                fila.Cells["colIni"].Value = $"({ini.GetX():0}, {ini.GetY():0})";
                fila.Cells["colFin"].Value = $"({fin.GetX():0}, {fin.GetY():0})";
                fila.Cells["colVel"].Value =plan.GetVelocidad().ToString("0");
            }
            ActualizarGrid();

            List<FlightPlan> vuelosEnConflicto = conflictDetector.ConflictosDetectados();

            // Colorear filas
            PintarConflictos(vuelosEnConflicto);
            if(vuelosEnConflicto != null) 
            {

            }
        }

        public void ActualizarGrid() 
        {
            foreach (DataGridViewRow fila in Frm_DataGrid.Rows)
            {
                FlightPlan plan = fila.Tag as FlightPlan;
                if (plan == null) continue;

                Position act = plan.GetcurrentPosition();
                fila.Cells["colAct"].Value = $"({act.GetX():0.0}, {act.GetY():0.0})";
                //fila.Cells["colEst"].Value = plan.HasArrived() ? "Llegado" : "En vuelo";
            }

            ActualizarConflictosGrid();
        }

        public void SeleccionarEnGrid(FlightPlan plan)
        {
            Frm_DataGrid.ClearSelection();

            if (plan == null) return;

            foreach (DataGridViewRow fila in Frm_DataGrid.Rows)
            {
                if (ReferenceEquals(fila.Tag, plan))
                {
                    fila.Selected = true;
                    Frm_DataGrid.CurrentCell = fila.Cells[0];

                    vueloSelecionado = plan;
                    Frm_Radar_Panel.Seleccionado = plan;

                    // Mostrar la fila seleccionada
                    Frm_DataGrid.FirstDisplayedScrollingRowIndex = fila.Index;

                    return;
                }
            }
        }

        private void Frm_DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            FlightPlan avion = Frm_DataGrid.Rows[e.RowIndex].Tag as FlightPlan;
            vueloSelecionado = avion;
            Frm_Radar_Panel.Seleccionado = avion;
            
            DataGridViewRow filaSeleccionada = Frm_DataGrid.Rows[e.RowIndex];
            filaSeleccionada.DefaultCellStyle.SelectionBackColor = Color.Orange;
            filaSeleccionada.DefaultCellStyle.SelectionForeColor = Color.FromArgb(10, 20, 40);
        }

        private void PintarConflictos(IEnumerable<FlightPlan> vuelosEnConflicto)
        {
            // Primero restauramos el color normal de todas las filas
            foreach (DataGridViewRow fila in Frm_DataGrid.Rows)
            {
                fila.DefaultCellStyle.SelectionBackColor = Color.Orange;
                fila.DefaultCellStyle.SelectionForeColor = Color.FromArgb(10, 20, 40);
            }

            // Pintamos de rojo los aviones implicados en conflictos
            foreach (DataGridViewRow fila in Frm_DataGrid.Rows)
            {
                FlightPlan plan = fila.Tag as FlightPlan;

                if (plan == null) continue;

                if (vuelosEnConflicto.Any(v => ReferenceEquals(v, plan)))
                {
                    fila.DefaultCellStyle.BackColor = Color.Red;
                    fila.DefaultCellStyle.ForeColor = Color.White;

                    // Conservamos el naranja cuando el usuario selecciona la fila
                    fila.DefaultCellStyle.SelectionBackColor = Color.Orange;
                    fila.DefaultCellStyle.SelectionForeColor = Color.FromArgb(10, 20, 40);
                }
            }
        }

        private void Frm_DataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (Frm_DataGrid.Columns[e.ColumnIndex].Name == "colVel")
            {
                DataGridViewRow fila = Frm_DataGrid.Rows[e.RowIndex];

                FlightPlan plan = fila.Tag as FlightPlan;

                if (plan == null) return;

                double velocidad;

                if (double.TryParse(
                    Convert.ToString(fila.Cells["colVel"].Value),
                    out velocidad))
                {
                    if (checker.check_vel(velocidad) == "0")
                    {
                        // Actualizamos la velocidad del avión
                        plan.SetVelocidad(velocidad);

                        // Actualizamos el valor visual
                        fila.Cells["colVel"].Value = velocidad.ToString("0");

                        // Comprobamos nuevamente los conflictos
                        ActualizarConflictosGrid();
                    }
                    else
                    {
                        MessageBox.Show("La velocidad introducida no es válida.");

                        fila.Cells["colVel"].Value =
                            plan.GetVelocidad().ToString("0");
                    }
                }
                else
                {
                    MessageBox.Show("Introduce una velocidad numérica.");

                    fila.Cells["colVel"].Value =
                        plan.GetVelocidad().ToString("0");
                }
            }
        }

        private void ActualizarConflictosGrid()
        {
            List<FlightPlan> conflictos =
                conflictDetector.ConflictosDetectados();

            foreach (DataGridViewRow fila in Frm_DataGrid.Rows)
            {
                FlightPlan plan = fila.Tag as FlightPlan;

                if (plan == null) continue;

                if (conflictos.Contains(plan))
                {
                    fila.DefaultCellStyle.BackColor = Color.Red;
                    fila.DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    fila.DefaultCellStyle.BackColor = Color.FromArgb(10, 20, 40);
                    fila.DefaultCellStyle.ForeColor = Color.Orange;
                }
            }
            Frm_Radar_Panel.Invalidate();
        }

        private void Frm_S_Download_Button_Click(object sender, EventArgs e)
        {
            // Se juntan los vuelos de planList en una lista
            List<FlightPlan> lista = new List<FlightPlan>();
            for (int i = 0; i < 10; i++)       // si aún tienes el límite de 10: i < 10
            {
                FlightPlan p = planList.GetFlightPlan(i);
                if (p != null) lista.Add(p);
            }

            if (lista.Count == 0)
            {
                av = new _03_Avisos("No hay vuelos que guardar.");
                av.ShowDialog(this);
                return;
            }

            using (SaveFileDialog selector = new SaveFileDialog())
            {
                selector.Title = "Guardar vuelos";
                selector.Filter = "Ficheros de texto (*.txt)|*.txt";
                selector.FileName = "vuelos.txt";
                selector.DefaultExt = "txt";

                if (selector.ShowDialog() != DialogResult.OK) return;   // el usuario canceló

                try
                {
                    reader.GuardarVuelos(selector.FileName, lista);
                    av = new _03_Avisos("Vuelos guardados: " + lista.Count);
                }
                catch (IOException)
                {
                    av = new _03_Avisos("No se pudo escribir el fichero (¿está abierto en otro programa?).");
                }
                catch (UnauthorizedAccessException)
                {
                    av = new _03_Avisos("No tienes permiso para guardar en esa carpeta.");
                }
                av.ShowDialog(this);
            }
        }
    }
}
