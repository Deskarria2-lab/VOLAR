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
        Image plane;
        public FrmDashboard(FrmLogin login)
        {
            InitializeComponent();
            this.planList = new FlightPlanList();
            this.plane = Properties.Resources.avion_verde;
            this.checker = new MethodChecker();
            this.login = login;
            this.volsCerregats = new List<FlightPlan>();
            cont = 1;
        }

        private void FrmExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            {                                                                           //
                id = Frm_ID_Text.Text;                                                        //  La variable ID tiene los datos introducidos en IdBox
                vel = Convert.ToDouble(Frm_Vel_Text.Text);                                    //  La variable vel tiene los datos introducidos en VelBox
                xo = Convert.ToDouble(Frm_X0_Text.Text);                                      //  La variable xo tiene los datos introducidos en XoBox
                yo = Convert.ToDouble(Frm_Y0_Text.Text);                                      //  La variable yo tiene los datos introducidos en YoBox;
                xf = Convert.ToDouble(Frm_Xf_Text.Text);                                      //  La variable xf tiene los datos introducidos en XfBox;
                yf = Convert.ToDouble(Frm_Y0_Text.Text);                                      //  La variable yf tiene los datos introducidos en YfBox;

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
                    {                                                                   //
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
                reader.CargarVuelosDesdeFichero();
                av = new _03_Avisos(reader.EstadoFichero());
                av.Show(this);
            }
        }

        private void Frm_S_Cargar_Button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++) 
            {
                FlightPlan plan = planList.GetFlightPlan(i);
                if (planList.GetFlightPlan(i) != null) 
                {
                    plan.Restart();
                    volsCerregats.Add(plan);
                    RadarPanel.
                }
            }
        }
    }
}
