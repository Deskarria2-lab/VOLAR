using FlightLib;
using FlightLib.Base_de_Datos;
using Pruebas_GUI_Volar.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pruebas_GUI_Volar
{
    public partial class _03_Espacio_Aereo : Form
    {
        Options menu;
        FlightPlan plane;
        Image avion_40;
        _05_Datos_Avion da;
        bool conflictoDetectado = false;
        private readonly List<FlightPlan> volsCarregats = new List<FlightPlan>();
        DataBase database;
        FlightPlanRepository repository;
        string emailUsuarioActual;
        FlightPlanHistory historial;
        bool historialIniciado = false;

        public _03_Espacio_Aereo(Options menu)
        {
            InitializeComponent();
            this.menu = menu;
            this.avion_40 = Properties.Resources.avion_40;

            // Inicialitzem la connexió amb la base de dades i el repositori.
            database = new DataBase();
            repository = new FlightPlanRepository(database);

            // Creem l'objecte que gestionarà l'historial de la simulació.
            historial = new FlightPlanHistory();

            // Connectem amb el mètode automàtic.
            simulacionTimer.Tick += SimulacionTimer_Tick;

            // Connectem els botons amb els mètodes.
            IniciarAutoSim.Click += InicarAutoSim_Click;
            DetenerAutoSim.Click += DetenerAutoSim_Click;

            EspAereo.Paint += new PaintEventHandler(EspAereo_Paint);
        }

        private void LoadBut_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < 10; i++)
            {
                FlightPlan plan = menu.planList.GetFlightPlan(i);

                if (plan != null && !volsCarregats.Contains(plan))
                {
                    plan.Restart();
                    volsCarregats.Add(plan);
                }
            }

            // Inicialitzem l'historial amb les posicions inicials dels avions.
            historial.Iniciar(menu.planList);
            historialIniciado = true;

            EspAereo.Invalidate();
            detectarConflicto(sender, e);
        }

        private void CicloBut_Click(object sender, EventArgs e)
        {
            foreach (FlightPlan plan in volsCarregats)
            {
                plan.Mover(menu.tCicl);
            }

            historial.RegistrarPas(menu.planList);     // Guardem el nou estat després del moviment.
            EspAereo.Invalidate();
            ComprovarDestino();
            ComprovarConflictes();
        }

        // Executa automàticament cada 500 ms.
        private void SimulacionTimer_Tick(object sender, EventArgs e)
        {
            // Recorrem les 10 posicions possibles de la llista de plans de vol.
            for (int i = 0; i < 10; i++)
            {
                // Comprovem que hi hagi un pla de vol
                if (menu.planList.GetFlightPlan(i) != null)
                {
                    // Movem l'avió segons el temps de cicle establert.
                    menu.planList.GetFlightPlan(i).Mover(menu.tCicl);
                }
            }

            historial.RegistrarPas(menu.planList);     // Guardem el nou estat després del moviment.
            // Actualitzem el dibuix per mostrar les noves posicions dels avions.
            EspAereo.Invalidate();
            ComprovarConflictes();
            ComprovarDestino();
        }

        //Inicia la simulació automàtica quan premem el botó
        private void InicarAutoSim_Click(object sender, EventArgs e)
        {
            // Posem en marxa el temporitzador.
            simulacionTimer.Start();
        }

        //Atura la simulació automàtica quan premem el botó
        private void DetenerAutoSim_Click(object sender, EventArgs e)
        {
            simulacionTimer.Stop();
        }

        private void EspAereo_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen rojo = new Pen(Color.Red, 3);

            foreach (FlightPlan plan in volsCarregats)
            {
                plane = plan;

                bool enConflicte = false;

                foreach (FlightPlan altreAvio in volsCarregats)
                {
                    if (altreAvio != plan && plan.Conflicto(altreAvio, menu.dSeg))
                    {
                        enConflicte = true;
                        break;
                    }
                }

                Point inicio = new Point
                        (
                        (int)plane.GetinitialPosition().GetX(),
                        (int)plane.GetinitialPosition().GetY()
                        );
                    Point final = new Point
                        (
                        (int)plane.GetfinalPosition().GetX(),
                        (int)plane.GetfinalPosition().GetY()
                        );
                    Point centro = new Point
                        ((int)plane.GetcurrentPosition().GetX(),
                         (int)plane.GetcurrentPosition().GetY()
                        );
                    float radio = ((float)menu.dSeg) / 2;
                    if (enConflicte)
                    {
                        using (SolidBrush vermellTranslucid = new SolidBrush(Color.FromArgb(110, Color.Red)))
                        {
                            g.FillEllipse(vermellTranslucid, centro.X - radio, centro.Y - radio, 2 * radio, 2 * radio);
                        }
                    }
                    g.DrawEllipse(rojo, centro.X - radio, centro.Y - radio, 2 * radio, 2 * radio);
                    g.DrawLine(rojo, inicio, final);
                    g.DrawImage(avion_40,
                                centro.X - avion_40.Width / 2,
                                centro.Y - avion_40.Height / 2);
                
            }
        }

        int ComprovarConflictes_Cont = 0;

        private void ComprovarConflictes()
        {

            for (int i = 0; i < 10; i++) // Recorrem les 10 posicions possibles de la llista de plans de vol (anirem comparant cada avió amb els seguents de la llista)
            {
                FlightPlan avio1 = menu.planList.GetFlightPlan(i);

                if (avio1 != null)
                {
                    for (int j = i + 1; j < 10; j++)
                    {
                        FlightPlan avio2 = menu.planList.GetFlightPlan(j);

                        if (avio2 != null)
                        {
                            if (ComprovarConflictes_Cont == 0 && avio1.Conflicto(avio2, menu.dSeg)) // Utilitzem el metode Conflicto de la classe FlightPlan (Fase 1)
                            {
                                simulacionTimer.Stop();
                                conflictoDetectado = true;
                                ComprovarConflictes_Cont = 1;
                                string avions = avio1.Getid() + " - " + avio2.Getid();
                                finestraConflictes finestra = new finestraConflictes(menu.planList, menu.dSeg, avions);
                                finestra.ShowDialog();
                                EspAereo.Invalidate();
                                return;
                            }
                        }
                    }
                }
            }
        }


        private void EspAereo_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                if (menu.planList.GetFlightPlan(i) != null)
                {
                    FlightPlan plan = menu.planList.GetFlightPlan(i);

                    double dx = e.X - plan.GetcurrentPosition().GetX();
                    double dy = e.Y - plan.GetcurrentPosition().GetY();
                    double dist = Math.Sqrt(dx * dx + dy * dy);

                    if (dist <= avion_40.Width / 2)
                    {
                        da = new _05_Datos_Avion
                            (
                                plan.Getid(),
                                Convert.ToString(plan.GetVelocidad()),
                                Convert.ToString(plan.GetcurrentPosition().GetX()),
                                Convert.ToString(plan.GetcurrentPosition().GetY())
                            );
                        da.Show();
                    }
                }
            }
        }
        private void DataGridBut_Click(object sender, EventArgs e)
        {
            FlightPlan primerAvio = menu.planList.GetFlightPlan(0);
            FlightPlan segonAvio = menu.planList.GetFlightPlan(1);

            // Obrim el formulari passant-li els dos avions reals
            Form_TablaVuelos ventanaTabla = new Form_TablaVuelos(primerAvio, segonAvio);
            ventanaTabla.ShowDialog();
        }

        private void detectarConflicto(object sender, EventArgs e)
        {

            // Creem l'objecte detector passant-li la llista i la distància de seguretat
            ConflictDetector detector = new ConflictDetector(menu.planList, menu.dSeg);

            // Cridem al mètode ConflictosDetectados(), que retorna una llista de textos amb els IDs
            List<string> llistaConfliges = detector.ConflictosDetectados();

            // Comprovem si la llista té elements (si conté algun conflicte)
            if (llistaConfliges.Count > 0)
            {
                string avions = string.Join(", ", llistaConfliges);
                finestraConflictes finestraConflictes = new finestraConflictes(menu.planList, menu.dSeg, avions);
                finestraConflictes.ShowDialog();
            }
        }

        private void ComprovarDestino()
        {
            int totalAvions = 0;
            int avionsArribats = 0;

            for (int i = 0; i < 10; i++)
            {
                FlightPlan avio = menu.planList.GetFlightPlan(i);
                if (avio != null)
                {
                    totalAvions++;

                    // Obtenim les coordenades actuals i finals
                    double xActual = avio.GetcurrentPosition().GetX();
                    double yActual = avio.GetcurrentPosition().GetY();
                    double xFinal = avio.GetfinalPosition().GetX();
                    double yFinal = avio.GetfinalPosition().GetY();

                    if (xActual == xFinal && yActual == yFinal)
                    {
                        avionsArribats++;
                    }
                }
            }

            if (avionsArribats == totalAvions)
            {
                simulacionTimer.Stop();
                MessageBox.Show("Tots els avions han arribat a la seva destinació");
            }
        }

        private void DetenerAutoSim_Click_1(object sender, EventArgs e)
        {

        }

        private void Restartbut_Click(object sender, EventArgs e)
        {

        }

        private void Restartbut_Click_1(object sender, EventArgs e)
        {
            simulacionTimer.Stop();
            conflictoDetectado = false;
            ComprovarConflictes_Cont = 0;

            foreach (FlightPlan plan in volsCarregats)
            {
                plan.Restart();
                plan.SetVelocidad(plan.GetVelocidadInicial());

            }

            // Reiniciem també l'historial
            historial.Iniciar(menu.planList);
            historialIniciado = true;

            EspAereo.Invalidate();
            detectarConflicto(sender, e);
        }

        private void CanviVelocitat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Canvia la velocitat de l'avió clicant i introduint un nou valor a la casella de velocitat");
            DataGridBut_Click(sender, e);
        }

        private void GuardarSimulación_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailUsuarioActual))
                {
                    MessageBox.Show("No s'ha identificat l'usuari actual.");
                    return;
                }

                if (!historialIniciado || historial.Count == 0)
                {
                    MessageBox.Show("No hi ha cap historial de simulació per guardar.");
                    return;
                }

                database.Iniciar();

                // Ens assegurem que la taula existeix.
                repository.CrearTabla();

                // Obtenim tots els estats de l'Stack en ordre cronològic.
                List<List<FlightPlanData>> estados =
                    historial.ObtenerEstadosPerGuardar();

                // Guardem l'historial complet de l'usuari.
                repository.GuardarHistorial(emailUsuarioActual, estados);

                MessageBox.Show("La simulació s'ha guardat correctament.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en guardar la simulació: ");
            }
            finally
            {
                database.Cerrar();
            }
        }
    }
}