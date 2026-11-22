using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        private readonly List<FlightPlan> volsCerregats;
        Timer reloj = new Timer { Interval = 33 };
        public FrmDashboard(FrmLogin login)
        {
            InitializeComponent();
            ConfigurarGrid();
            reader = new GesitonFicheros();
            this.planList = new FlightPlanList();
            this.checker = new MethodChecker();
            this.login = login;
            this.volsCerregats = new List<FlightPlan>();
            cont = 1;
            Frm_Editor_Aviones.Visible = true;
            Frm_Dist_Text.Text = "50";
            Frm_Time_Text.Text = "5";
            reloj.Tick += Reloj_Tick;
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
                dSeg = Convert.ToDouble(Frm_Dist_Text.Text);                                  //  Guardamos los datos introducidos en DistBox
                tCicl = Convert.ToDouble(Frm_Time_Text.Text);                                 //  Guardamos los datos introducidos en TimeBox
                                                                                        //
                if (checker.check_dseg(dSeg) != "0")                                    //  En caso que no tengamos un okey con el checker dist seguridad
                {                                                                       //
                    av = new _03_Avisos(checker.check_dseg(dSeg));                       //  Crea la ventana de avisos con los datos del checker
                    av.Show(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else if (checker.check_tcicl(tCicl) != "0")                             //  En caso que no tengamos un okey con el checker tiempo de ciclo
                {                                                                       //
                    av = new _03_Avisos(checker.check_tcicl(tCicl));                     //  Crea la ventana de avisos con los datos del checker
                    av.Show(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else                                                                    //  Si todo esta bien
                {                                                                       //
                    av = new _03_Avisos("Datos de simulacion Guardados!");              //  Crea la ventana de avisos indicando que los datos para la simulacion estan bien guardados
                    av.Show(this);                                                      //  Abre la ventana de avisos
                }   //
                id = Frm_ID_Text.Text;                                                        //  La variable ID tiene los datos introducidos en IdBox
                vel = Convert.ToDouble(Frm_Vel_Text.Text);                                    //  La variable vel tiene los datos introducidos en VelBox
                xo = Convert.ToDouble(Frm_X0_Text.Text);                                      //  La variable xo tiene los datos introducidos en XoBox
                yo = Convert.ToDouble(Frm_Y0_Text.Text);                                      //  La variable yo tiene los datos introducidos en YoBox;
                xf = Convert.ToDouble(Frm_Xf_Text.Text);                                      //  La variable xf tiene los datos introducidos en XfBox;
                yf = Convert.ToDouble(Frm_Yf_Text.Text);                                      //  La variable yf tiene los datos introducidos en YfBox;

                if (checker.check_id(id) != "0")                                        //  Si el checker del Id no devuelve un okey
                {                                                                       //
                    av = new _03_Avisos(checker.check_id(id));                           //  Crea la ventana de avisos con los datos del checker
                    av.Show(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else if (checker.check_vel(vel) != "0")                                 //  Si el checker de la velocidad no devuelve un okey
                {                                                                       //
                    av = new _03_Avisos(checker.check_vel(vel));                         //  Crea la ventana de avisos con los datos del checker;
                    av.Show(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else if (checker.check_pos(xo, yo, xf, yf) != "0")                      //  Si el checker de las posiciones no devuelve un okey
                {                                                                       //
                    av = new _03_Avisos(checker.check_pos(xo, yo, xf, yf));              //  Crea la ventana de avisos con los datos del checker
                    av.Show(this);                                                      //  Abre la ventana de avisos
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
                        av.Show(this);                                                  //  Abre la ventana de avisos
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
                        av.Show(this);                                                  //  Abre la ventana de avisos
                    }                                                                   //
                }                                                                       //
            }
            catch (FormatException)                                                    //  En Caso de error de formato
            {
                av = new _03_Avisos(                                                    //  Crea la ventana avisando
                    "No se han introducido los datos con su formato correcto!");        //  Que los datos introducidos no son correctos
                av.Show(this);                                                          //  Muestra esta ventana//
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
                av.Show(this);
            }
        }
        ///////////////////////////////////////////// Simulation Control, funciones para dibujar aviones /////////////////////////////////////
        
        public void AvanzarCiclo() 
        {
            foreach (FlightPlan plan in volsCerregats)
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
            volsCerregats.Clear();
            for (int i = 0; i < 10; i++) 
            {
                FlightPlan plan = planList.GetFlightPlan(i);
                if (planList.GetFlightPlan(i) != null) 
                {
                    plan.Restart();
                    volsCerregats.Add(plan);
                    Frm_Radar_Panel.Vuelos.Add(plan);
                }
            }
            Frm_Radar_Panel.Invalidate();
            CargarGrid();
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

            foreach (FlightPlan plan in volsCerregats)
            {
                plan.Restart();
                plan.SetVelocidad(plan.GetVelocidadInicial());

            }

            Frm_Radar_Panel.Invalidate();
            ActualizarGrid();
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

            Frm_DataGrid.ReadOnly = true;
            Frm_DataGrid.AllowUserToAddRows = false;

            typeof(DataGridView).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(Frm_DataGrid, true, null);
        }

        public void CargarGrid() 
        {
            Frm_DataGrid.Columns.Clear();
            if(Frm_DataGrid.Columns.Count == 0 ) ConfigurarGrid();
            foreach (FlightPlan plan in volsCerregats) 
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
        }
    }
}
