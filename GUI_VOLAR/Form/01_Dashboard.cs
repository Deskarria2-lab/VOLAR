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
        bool conflicto;
        List<FlightPlan> vuelosEnConflicto;
        public FlightPlanList vuelosSolucionados { get; set; }
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
            conflicto = false;
            vuelosEnConflicto = new List<FlightPlan>();

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
            Application.Exit(); // Tanca l'aplicació quan l'usuari prem el botó de sortida.
        }
        private void Frm_Editor_Button_Click(object sender, EventArgs e)
        {
            Frm_Editor_Aviones.Visible = true; // Mostra el panell per editar els avions.
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
            cont = 1; // Indica que s'està configurant l'avió A.
            Frm_Editor_Aviones.Visible = true; // Mostra el panell de configuració.
            Frm_Avion_Label.Text = "Avion A:"; // Identifica el formulari com a configuració de l'avió A.
            Frm_ID_Text.Text = null; // Neteja el camp de l'identificador.
            Frm_Vel_Text.Text = null; // Neteja el camp de la velocitat.
            Frm_X0_Text.Text = null; // Neteja el camp de la coordenada inicial X.
            Frm_Y0_Text.Text = null; // Neteja el camp de la coordenada inicial Y.
            Frm_Xf_Text.Text = null; // Neteja el camp de la coordenada final X.
            Frm_Yf_Text.Text = null; // Neteja el camp de la coordenada final Y.

        }

        private void Frm_Conf_AvionB_Button_Click(object sender, EventArgs e)
        {
            cont = 2; // Indica que s'està configurant l'avió B.
            Frm_Editor_Aviones.Visible = true; // Mostra el panell de configuració.
            Frm_Avion_Label.Text = "Avion B:"; // Identifica el formulari com a configuració de l'avió B.
            Frm_ID_Text.Text = null; // Neteja el camp de l'identificador.
            Frm_Vel_Text.Text = null; // Neteja el camp de la velocitat.
            Frm_X0_Text.Text = null; // Neteja el camp de la coordenada inicial X.
            Frm_Y0_Text.Text = null; // Neteja el camp de la coordenada inicial Y.
            Frm_Xf_Text.Text = null; // Neteja el camp de la coordenada final X.
            Frm_Yf_Text.Text = null; // Neteja el camp de la coordenada final Y.
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
                    av = new _03_Avisos(checker.check_id(id), conflicto, vuelosEnConflicto, dSeg);                           //  Crea la ventana de avisos con los datos del checker
                    av.ShowDialog(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else if (checker.check_vel(vel) != "0")                                 //  Si el checker de la velocidad no devuelve un okey
                {                                                                       //
                    av = new _03_Avisos(checker.check_vel(vel), conflicto, vuelosEnConflicto, dSeg);                        //  Crea la ventana de avisos con los datos del checker;
                    av.ShowDialog(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else if (checker.check_pos(xo, yo, xf, yf) != "0")                      //  Si el checker de las posiciones no devuelve un okey
                {                                                                       //
                    av = new _03_Avisos(checker.check_pos(xo, yo, xf, yf), conflicto, vuelosEnConflicto, dSeg);  //  Crea la ventana de avisos con los datos del checker
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
                        av = new _03_Avisos("El Vuelo A esta creado!", conflicto, vuelosEnConflicto, dSeg);                  //  Crea la ventana de avisando del vuelo creado
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
                        av = new _03_Avisos("El Vuelo B esta creado!", conflicto, vuelosEnConflicto, dSeg);                  //  Crea la ventana de avisando del vuelo creado
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
                    "No se han introducido los datos con su formato correcto!", conflicto, vuelosEnConflicto, dSeg);        //  Que los datos introducidos no son correctos
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
                    av = new _03_Avisos(checker.check_dseg(dSeg), conflicto, vuelosEnConflicto, dSeg);                       //  Crea la ventana de avisos con los datos del checker
                    av.ShowDialog(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else if (checker.check_tcicl(tCicl) != "0")                             //  En caso que no tengamos un okey con el checker tiempo de ciclo
                {                                                                       //
                    av = new _03_Avisos(checker.check_tcicl(tCicl), conflicto, vuelosEnConflicto, dSeg);                     //  Crea la ventana de avisos con los datos del checker
                    av.ShowDialog(this);                                                      //  Abre la ventana de avisos
                }                                                                       //
                else                                                                    //  Si todo esta bien
                {                                                                       //
                    av = new _03_Avisos("Datos de simulacion Guardados!", conflicto, vuelosEnConflicto, dSeg);              //  Crea la ventana de avisos indicando que los datos para la simulacion estan bien guardados
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
                    "No se han introducido los datos con su formato correcto!", conflicto, vuelosEnConflicto, dSeg);        //  Que los datos introducidos no son correctos
                av.ShowDialog(this);
            }

        }

        private void Frm_Conf_Auto_Button_Click(object sender, EventArgs e)
        {
            if(cont == 1) // Carrega les dades de prova de l'avió A quan aquest és l'avió seleccionat.
            {
                Frm_ID_Text.Text = "AA1234";
                Frm_Vel_Text.Text = "50";
                Frm_X0_Text.Text = "0";
                Frm_Y0_Text.Text = "0";
                Frm_Xf_Text.Text = "590";
                Frm_Yf_Text.Text = "590";
            }
            if(cont == 2) // Carrega les dades de prova de l'avió B quan aquest és l'avió seleccionat.
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
            // Obre un selector de fitxers i el tanca automàticament quan acaba l'operació.
            using (OpenFileDialog selector = new OpenFileDialog())
            {
                selector.Title = "Selecciona el fitxer de vols";
                selector.Filter =
                    "Fitxers de text (*.txt)|*.txt|" +
                    "Fitxers CSV (*.csv)|*.csv|" +
                    "Tots els fitxers (*.*)|*.*";

                if (selector.ShowDialog() != DialogResult.OK) // Comprova si l'usuari ha confirmat la selecció.
                {
                    return; // Surt sense carregar res si s'ha cancel·lat el selector.
                }
                reader.GestionarFicheros(selector.FileName); // Llegeix el fitxer que s'ha seleccionat.
                planList = reader.CargarVuelosDesdeFichero(); // Converteix les dades del fitxer en plans de vol.
                av = new _03_Avisos(reader.EstadoFichero(), conflicto, vuelosEnConflicto, dSeg); // Prepara un avís amb el resultat de la lectura.
                aviones_cargados = true; // Indica que ja s'han carregat els avions.
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
                av.ShowDialog(this); // Mostra el resultat de carregar el fitxer.
            }
        }
        ///////////////////////////////////////////// Simulation Control, funciones para dibujar aviones /////////////////////////////////////
        
        public void GuardarPosiciones() 
        {
            // Crea una còpia de les posicions actuals per poder desfer un cicle.
            Position[] foto = new Position[volsCarregats.Count];
            for (int i = 0; i < foto.Length; i++) // Recorre tots els avions per guardar-ne la posició.
            {
                foto[i] = volsCarregats[i].GetcurrentPosition(); // Desa la posició actual de l'avió a la còpia.
            }
            positions.Push(foto); // Afegeix la còpia a l'historial de posicions.
        }

        public void DeshacerPaso() 
        {
            if (positions.Count == 0) return; // No pot desfer cap pas si l'historial és buit.

            reloj.Stop(); // Atura la simulació abans de restaurar les posicions.
            Position[] foto = positions.Pop(); // Recupera l'última còpia desada.
            for (int i = 0; i < foto.Length && i < volsCarregats.Count; i++) // Recorre només les posicions i avions disponibles.
            {
                volsCarregats[i].SetcurrentPosition(foto[i]); // Restaura la posició anterior de cada avió.
            }
            Frm_Radar_Panel.Invalidate(); // Redibuixa el radar amb les posicions restaurades.
            ActualizarGrid(); // Actualitza la graella amb les posicions restaurades.
        }
        public void AvanzarCiclo() 
        {
            bool mov = false;
            foreach (FlightPlan plan in volsCarregats) // Comprova si algun avió encara no ha arribat al destí.
            {
                if (!plan.HasArrived()){ mov = true; break; } // Marca que hi ha moviment pendent i deixa de comprovar avions.
            }

            if (!mov) return; // No avança la simulació si tots els avions ja han arribat.
            GuardarPosiciones(); // Desa les posicions abans de moure els avions.
            foreach (FlightPlan plan in volsCarregats) // Recorre els avions carregats a la simulació.
            {
                plan.Mover(tCicl); // Mou l'avió durant el temps de cicle configurat.
            }
            Frm_Radar_Panel.Invalidate(); // Redibuixa al radar les noves posicions.
            ActualizarGrid(); // Actualitza la informació de la graella.
        }
        private void Reloj_Tick(object sender, EventArgs e) 
        {
            AvanzarCiclo(); // Avança un cicle quan el temporitzador genera un tic.
        }
        private void Frm_S_Cargar_Button_Click(object sender, EventArgs e)
        {
            Frm_Radar_Panel.Vuelos.Clear(); // Elimina del radar els avions carregats anteriorment.
            volsCarregats.Clear(); // Buida la llista d'avions de la simulació.
            positions.Clear(); // Neteja l'historial de posicions anterior.
            for (int i = 0; i < 10; i++) // Recorre els espais de plans de vol disponibles.
            {
                FlightPlan plan = planList.GetFlightPlan(i); // Recupera el pla de vol de la posició actual.
                if (planList.GetFlightPlan(i) != null) // Continua només si hi ha un avió configurat.
                {
                    plan.Restart(); // Retorna l'avió a la posició inicial.
                    volsCarregats.Add(plan); // Afegeix l'avió a la llista de simulació.
                    Frm_Radar_Panel.Vuelos.Add(plan); // Afegeix l'avió a la col·lecció que dibuixa el radar.
                }
            }
            conflictDetector = new ConflictDetector(planList, dSeg); // Crea el detector amb la distància de seguretat actual.
            Frm_Radar_Panel.Invalidate(); // Redibuixa els avions al radar.
            GuardarPosiciones(); // Desa les posicions inicials per permetre desfer un pas.
            CargarGrid(); // Mostra els avions carregats a la graella.
            datos_cargados = true; // Indica que la simulació ja té les dades preparades.
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
            AvanzarCiclo(); // Avança manualment un sol cicle de la simulació.
        }

        private void Frm_S_Start_Button_Click(object sender, EventArgs e)
        {
            reloj.Start(); // Inicia el temporitzador que mou els avions periòdicament.
        }

        private void Frm_S_Stop_Button_Click(object sender, EventArgs e)
        {
            reloj.Stop(); // Atura el temporitzador i pausa el moviment dels avions.
        }

        private void Frm_S_Restart_Button_Click(object sender, EventArgs e)
        {
            reloj.Stop(); // Atura la simulació abans de reiniciar-la.
            positions.Clear(); // Esborra les posicions guardades de la simulació anterior.

            foreach (FlightPlan plan in volsCarregats) // Reinicia cada avió que participa en la simulació.
            {
                plan.Restart(); // Torna l'avió a la seva posició inicial.
                plan.SetVelocidad(plan.GetVelocidadInicial()); // Recupera la velocitat inicial de l'avió.

            }

            Frm_Radar_Panel.Invalidate(); // Redibuixa el radar després del reinici.
            ActualizarGrid(); // Actualitza la graella amb les dades reiniciades.
        }

        private void Frm_S_PasoAnterior_Click(object sender, EventArgs e)
        {
            DeshacerPaso(); // Restaura les posicions del pas anterior.
        }

        private void Radar_VueloSeleccionado(object sender, FlightPlan plan) 
        {
            vueloSelecionado = plan; // Desa quin avió s'ha seleccionat al radar.
            SeleccionarEnGrid(plan); // Selecciona també la fila corresponent a la graella.
        }

        public void RefrescarVista()
        {
            ActualizarGrid(); // Refresca les dades de la graella.
            Frm_Radar_Panel.Invalidate(); // Demana al radar que torni a dibuixar-se.
        }


        public void AplicarVelocidades(List<FlightPlan> vuelosModificados)
        {
            if (vuelosModificados == null) return; // Surt si no s'ha rebut cap llista de velocitats modificades.

            // Actualizamos los vuelos que están cargados en la simulación
            foreach (FlightPlan original in volsCarregats) // Recorre els avions que ja són a la simulació.
            {
                FlightPlan modificado = vuelosModificados.FirstOrDefault(
                    v => v.Getid() == original.Getid()
                ); // Cerca el vol modificat amb el mateix identificador.

                if (modificado != null) // Actualitza només si s'ha trobat una versió modificada.
                {
                    original.SetVelocidad(modificado.GetVelocidad()); // Aplica la nova velocitat a l'avió carregat.
                }
            }

            // Actualizamos también los vuelos de la lista principal
            for (int i = 0; i < 10; i++) // Recorre els plans de vol de la llista principal.
            {
                FlightPlan original = planList.GetFlightPlan(i); // Recupera el pla de la posició actual.

                if (original == null) continue; // Salta la posició si no conté cap pla de vol.

                FlightPlan modificado = vuelosModificados.FirstOrDefault(
                    v => v.Getid() == original.Getid()
                ); // Cerca la versió modificada que correspon al mateix identificador.

                if (modificado != null) // Actualitza només els plans que apareixen a la llista modificada.
                {
                    original.SetVelocidad(modificado.GetVelocidad()); // Desa la velocitat nova al pla principal.
                }
            }

            // Actualizamos la interfaz
            RefrescarVista(); // Mostra a la interfície les velocitats actualitzades.
        }

        public List<FlightPlan> ResolverConflictos()
        {
            if (conflictDetector == null) // Evita resoldre conflictes abans de crear el detector.
                return new List<FlightPlan>();

            List<FlightPlan> cambiados = conflictDetector.ResolverConflictos(); // Demana al detector que ajusti les velocitats necessàries.

            // Actualizamos la tabla y el radar
            ActualizarGrid(); // Actualitza la graella amb els canvis del detector.

            // Volvemos a comprobar los conflictos
            vuelosEnConflicto = conflictDetector.ConflictosDetectados(); // Torna a calcular quins vols continuen en conflicte.
            vuelosEnConflicto = conflictDetector.ConflictosDetectados(); // Actualitza de nou la llista de conflictes detectats.

            conflicto = vuelosEnConflicto.Count > 0; // Indica si encara queda algun conflicte.

            PintarConflictos(vuelosEnConflicto); // Ressalta a la graella els avions que encara tenen conflictes.

            Frm_Radar_Panel.Invalidate(); // Redibuixa el radar amb els canvis aplicats.

            return cambiados; // Retorna els avions als quals s'ha modificat la velocitat.
        }

        ///////////////////////////////////////////// DATA GRID ////////////////////////////////////
        public void ConfigurarGrid() 
        {
            Frm_DataGrid.Columns.Clear(); // Elimina les columnes actuals abans de configurar la graella.
            Frm_DataGrid.Columns.Add("ColId", "ID"); // Afegeix la columna de l'identificador.
            Frm_DataGrid.Columns.Add("ColIni", "Pos. Inicial"); // Afegeix la columna de la posició inicial.
            Frm_DataGrid.Columns.Add("ColFin", "Pos. Final"); // Afegeix la columna de la posició final.
            Frm_DataGrid.Columns.Add("ColAct", "Pos. Actual"); // Afegeix la columna de la posició actual.
            Frm_DataGrid.Columns.Add("colVel", "Velocidad"); // Afegeix la columna de la velocitat.

            foreach (DataGridViewColumn columna in Frm_DataGrid.Columns) // Recorre les columnes per bloquejar-les per defecte.
            {
                columna.ReadOnly = true; // Impedeix l'edició de la columna actual.
            }

            // Solo velocidad editable
            Frm_DataGrid.Columns["colVel"].ReadOnly = false; // Permet editar la velocitat, mantenint les altres columnes bloquejades.
            Frm_DataGrid.AllowUserToAddRows = false; // Evita que s'afegeixin files que no corresponguin a vols.
            Frm_DataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona la fila sencera quan s'hi fa clic.
            Frm_DataGrid.MultiSelect = false; // Limita la selecció a una sola fila.
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
            Frm_DataGrid.Columns.Clear(); // Neteja les columnes per reconstruir la graella de vols.
            if(Frm_DataGrid.Columns.Count == 0 ) ConfigurarGrid(); // Crea les columnes si encara no estan configurades.
            foreach (FlightPlan plan in volsCarregats) // Afegeix a la graella cada avió carregat a la simulació.
            {
                int n = Frm_DataGrid.Rows.Add(); // Crea una fila nova per a l'avió.
                DataGridViewRow fila = Frm_DataGrid.Rows[n]; // Recupera la fila que s'acaba de crear.
                fila.Tag = plan; // Enllaça la fila amb el pla de vol de l'avió.

                Position ini = plan.GetinitialPosition(), fin = plan.GetfinalPosition();
                fila.Cells["colId"].Value = plan.Getid(); // Mostra l'identificador del vol.
                fila.Cells["colIni"].Value = $"({ini.GetX():0}, {ini.GetY():0})"; // Mostra les coordenades inicials.
                fila.Cells["colFin"].Value = $"({fin.GetX():0}, {fin.GetY():0})"; // Mostra les coordenades del destí.
                fila.Cells["colVel"].Value =plan.GetVelocidad().ToString("0"); // Mostra la velocitat actual.
                if (!fila.Cells["colVel"].IsInEditMode) // Evita sobreescriure una velocitat que s'està editant.
                {
                    fila.Cells["colVel"].Value = plan.GetVelocidad().ToString("0"); // Actualitza la velocitat visible de la fila.
                }
            }
            ActualizarGrid(); // Omple també les posicions actuals i revisa els conflictes.

            vuelosEnConflicto = conflictDetector.ConflictosDetectados(); // Calcula els conflictes dels avions carregats.

            // Colorear filas
            PintarConflictos(vuelosEnConflicto); // Ressalta les files dels avions en conflicte.
            if(vuelosEnConflicto != null) // Mostra l'avís quan el detector retorna una llista de conflictes.
            {
                conflicto = true; // Indica que s'han detectat conflictes.
                av = new _03_Avisos("Hay conflictos! Desea intentar solucionar los conflictos?", conflicto, vuelosEnConflicto, dSeg); // Prepara l'avís per a l'usuari.
                av.ShowDialog(this); // Mostra l'avís sobre els conflictes detectats.
            }
        }

        public void ActualizarGrid()
        {
            foreach (DataGridViewRow fila in Frm_DataGrid.Rows) // Actualitza cada fila de la graella.
            {
                FlightPlan plan = fila.Tag as FlightPlan; // Recupera el pla de vol associat a la fila.
                if (plan == null) continue; // Salta la fila si no està associada a cap avió.

                Position act = plan.GetcurrentPosition(); // Obté la posició actual de l'avió.

                // Actualizar posición actual
                fila.Cells["colAct"].Value = // Escriu les coordenades actuals a la columna corresponent.
                    $"({act.GetX():0.0}, {act.GetY():0.0})";

                // Actualizar velocidad
                if (!fila.Cells["colVel"].IsInEditMode) // No altera la cel·la mentre l'usuari l'està editant.
                {
                    fila.Cells["colVel"].Value = // Actualitza la velocitat mostrada a la graella.
                        plan.GetVelocidad().ToString("0.##");
                }
            }

            ActualizarConflictosGrid(); // Actualitza els colors de les files segons els conflictes actuals.
        }

        public void SeleccionarEnGrid(FlightPlan plan)
        {
            Frm_DataGrid.ClearSelection(); // Desselecciona la fila activa abans de buscar la nova.

            if (plan == null) return; // Surt si no s'ha proporcionat cap avió.

            foreach (DataGridViewRow fila in Frm_DataGrid.Rows) // Busca la fila vinculada al pla de vol indicat.
            {
                if (ReferenceEquals(fila.Tag, plan)) // Comprova que la fila correspon exactament a aquest avió.
                {
                    fila.Selected = true; // Selecciona la fila de l'avió.
                    Frm_DataGrid.CurrentCell = fila.Cells[0]; // Situa la cel·la activa a la primera columna.

                    vueloSelecionado = plan; // Actualitza l'avió seleccionat al formulari.
                    Frm_Radar_Panel.Seleccionado = plan; // Sincronitza la selecció del radar.

                    // Mostrar la fila seleccionada
                    Frm_DataGrid.FirstDisplayedScrollingRowIndex = fila.Index; // Desplaça la graella fins a mostrar la fila.

                    return; // Acaba la cerca després de trobar l'avió.
                }
            }
        }

        private void Frm_DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignora els clics sobre la capçalera o fora de les files.

            FlightPlan avion = Frm_DataGrid.Rows[e.RowIndex].Tag as FlightPlan; // Recupera l'avió associat a la fila clicada.
            vueloSelecionado = avion; // Desa l'avió seleccionat al formulari.
            Frm_Radar_Panel.Seleccionado = avion; // Actualitza la selecció que es mostra al radar.
            
            DataGridViewRow filaSeleccionada = Frm_DataGrid.Rows[e.RowIndex]; // Recupera la fila per personalitzar-ne la selecció.
            filaSeleccionada.DefaultCellStyle.SelectionBackColor = Color.Orange; // Ressalta el fons de la fila seleccionada.
            filaSeleccionada.DefaultCellStyle.SelectionForeColor = Color.FromArgb(10, 20, 40); // Ajusta el color del text seleccionat.
        }

        private void PintarConflictos(IEnumerable<FlightPlan> vuelosEnConflicto)
        {
            // Primero restauramos el color normal de todas las filas
            foreach (DataGridViewRow fila in Frm_DataGrid.Rows) // Restaura primer l'estil de selecció de totes les files.
            {
                fila.DefaultCellStyle.SelectionBackColor = Color.Orange;
                fila.DefaultCellStyle.SelectionForeColor = Color.FromArgb(10, 20, 40);
            }

            // Pintamos de rojo los aviones implicados en conflictos
            foreach (DataGridViewRow fila in Frm_DataGrid.Rows) // Recorre les files per identificar els avions en conflicte.
            {
                FlightPlan plan = fila.Tag as FlightPlan; // Recupera el pla associat a la fila.

                if (plan == null) continue; // Salta les files sense cap pla de vol associat.

                if (vuelosEnConflicto.Any(v => ReferenceEquals(v, plan))) // Comprova si aquest avió forma part d'algun conflicte.
                {
                    fila.DefaultCellStyle.BackColor = Color.Red; // Pinta de vermell el fons de l'avió en conflicte.
                    fila.DefaultCellStyle.ForeColor = Color.White; // Canvia el text a blanc per mantenir-ne el contrast.

                    // Conservamos el naranja cuando el usuario selecciona la fila
                    fila.DefaultCellStyle.SelectionBackColor = Color.Orange;
                    fila.DefaultCellStyle.SelectionForeColor = Color.FromArgb(10, 20, 40);
                }
            }
        }

        private void Frm_DataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignora l'esdeveniment si no correspon a una fila de dades.

            if (Frm_DataGrid.Columns[e.ColumnIndex].Name == "colVel") // Valida només les modificacions de la columna de velocitat.
            {
                DataGridViewRow fila = Frm_DataGrid.Rows[e.RowIndex]; // Recupera la fila que s'ha editat.

                FlightPlan plan = fila.Tag as FlightPlan; // Recupera l'avió associat a la fila.

                if (plan == null) return; // Surt si la fila no té cap avió associat.

                double velocidad;

                if (double.TryParse( // Intenta convertir el contingut de la cel·la a un nombre decimal.
                    Convert.ToString(fila.Cells["colVel"].Value),
                    out velocidad))
                {
                    if (checker.check_vel(velocidad) == "0") // Continua només si el validador accepta la velocitat.
                    {
                        // Actualizamos la velocidad del avión
                        plan.SetVelocidad(velocidad); // Aplica la velocitat introduïda a l'avió.

                        // Actualizamos el valor visual
                        fila.Cells["colVel"].Value = velocidad.ToString("0"); // Mostra el valor validat a la cel·la.

                        // Comprobamos nuevamente los conflictos
                        ActualizarConflictosGrid(); // Recalcula i redibuixa els conflictes després del canvi.
                    }
                    else
                    {
                        MessageBox.Show("La velocidad introducida no es válida."); // Informa que la velocitat no supera la validació.

                        fila.Cells["colVel"].Value = // Restaura a la cel·la l'última velocitat vàlida.
                            plan.GetVelocidad().ToString("0");
                    }
                }
                else
                {
                    MessageBox.Show("Introduce una velocidad numérica."); // Informa que cal introduir un valor numèric.

                    fila.Cells["colVel"].Value = // Restaura a la cel·la l'última velocitat vàlida.
                        plan.GetVelocidad().ToString("0");
                }
            }
        }

        private void ActualizarConflictosGrid()
        {
            List<FlightPlan> conflictos = // Obté els conflictes actuals per actualitzar els colors de la graella.
                conflictDetector.ConflictosDetectados();

            foreach (DataGridViewRow fila in Frm_DataGrid.Rows) // Recorre totes les files de vols per aplicar-ne l'estat visual.
            {
                FlightPlan plan = fila.Tag as FlightPlan; // Recupera l'avió associat a la fila.

                if (plan == null) continue; // Salta les files que no representen cap avió.

                if (conflictos.Contains(plan)) // Aplica l'estil d'alerta als avions que tenen conflicte.
                {
                    fila.DefaultCellStyle.BackColor = Color.Red; // Pinta de vermell el fons de la fila conflictiva.
                    fila.DefaultCellStyle.ForeColor = Color.White; // Mostra el text en blanc sobre el fons vermell.
                }
                else // Restaura els colors habituals quan l'avió no té conflictes.
                {
                    fila.DefaultCellStyle.BackColor = Color.FromArgb(10, 20, 40); // Aplica el color normal de fons.
                    fila.DefaultCellStyle.ForeColor = Color.Orange; // Aplica el color normal del text.
                }
            }
            Frm_Radar_Panel.Invalidate(); // Redibuixa el radar perquè reflecteixi l'estat actual dels conflictes.
        }

        private void Frm_S_Download_Button_Click(object sender, EventArgs e)
        {
            // Se juntan los vuelos de planList en una lista
            List<FlightPlan> lista = new List<FlightPlan>();
            for (int i = 0; i < 10; i++) // Recorre els espais disponibles per recopilar els plans de vol.
            {
                FlightPlan p = planList.GetFlightPlan(i); // Recupera el pla de vol de la posició actual.
                if (p != null) lista.Add(p); // Afegeix a la llista els plans que existeixen.
            }

            if (lista.Count == 0) // Comprova si hi ha vols abans d'obrir el selector per desar.
            {
                av = new _03_Avisos("No hay vuelos que guardar.", conflicto, vuelosEnConflicto, dSeg); // Prepara un avís si no hi ha dades.
                av.ShowDialog(this); // Informa l'usuari que no hi ha vols per desar.
                return; // Surt perquè no hi ha res a escriure.
            }

            // Obre el selector per triar on desar els vols i el tanca en acabar.
            using (SaveFileDialog selector = new SaveFileDialog())
            {
                selector.Title = "Guardar vuelos";
                selector.Filter = "Ficheros de texto (*.txt)|*.txt";
                selector.FileName = "vuelos.txt";
                selector.DefaultExt = "txt";

                if (selector.ShowDialog() != DialogResult.OK) return; // Surt si l'usuari cancel·la el desament.

                // Gestiona els possibles errors d'escriptura del fitxer.
                try
                {
                    reader.GuardarVuelos(selector.FileName, lista); // Desa els plans de vol al fitxer seleccionat.
                    av = new _03_Avisos("Vuelos guardados: " + lista.Count, conflicto, vuelosEnConflicto, dSeg); // Prepara la confirmació amb el nombre de vols.
                }
                catch (IOException)
                {
                    av = new _03_Avisos("No se pudo escribir el fichero (¿está abierto en otro programa?).", conflicto, vuelosEnConflicto, dSeg); // Informa d'un error en escriure el fitxer.
                }
                catch (UnauthorizedAccessException)
                {
                    av = new _03_Avisos("No tienes permiso para guardar en esa carpeta.", conflicto, vuelosEnConflicto, dSeg); // Informa que no hi ha permisos per desar a la carpeta.
                }
                av.ShowDialog(this); // Mostra el resultat del desament o l'error detectat.
            }
        }
    }
}
