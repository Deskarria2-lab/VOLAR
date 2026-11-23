using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FlightLib;

namespace GUI_VOLAR
{
    public class RadarPanel : Panel
    {
        // ---------- Cuadrícula ----------
        public const int Lado = 600;          // coordenadas válidas: de 0 a 599
        public int Celdas { get; set; } = 12;

        Color _colorLineas = Color.FromArgb(0, 200, 0);
        public Color ColorLineas
        {
            get { return _colorLineas; }
            set { _colorLineas = value; Invalidate(); }
        }

        // ---------- Aviones ----------
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<FlightPlan> Vuelos { get; } = new List<FlightPlan>();

        FlightPlan _seleccionado;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FlightPlan Seleccionado
        {
            get { return _seleccionado; }
            set { _seleccionado = value; Invalidate(); }
        }

        // Se lanza al pulsar el radar. El vuelo es null si no se ha pulsado ningún avión.
        public event EventHandler<FlightPlan> VueloSeleccionado;

        public float EscalaAvion { get; set; } = 1.0f;
        public int RadioClic { get; set; } = 14;               // píxeles de margen para acertar un avión
        public bool TrayectoriasTodas { get; set; } = true;    // false = solo la del seleccionado
        public bool MostrarSeguridad { get; set; } = true;

        double _distanciaSeguridad = 20;                       // en unidades del mapa
        public double DistanciaSeguridad
        {
            get { return _distanciaSeguridad; }
            set { _distanciaSeguridad = value; Invalidate(); }
        }

        // ---------- Pinceles y lápices (se crean una sola vez) ----------
        readonly Brush _pincelAvion = new SolidBrush(Color.FromArgb(0, 255, 0));
        readonly Brush _pincelSel = new SolidBrush(Color.FromArgb(255, 138, 31));
        readonly Font _fuente = new Font("Consolas", 9f, FontStyle.Bold);

        readonly Pen _penRecorrido = new Pen(Color.FromArgb(90, 0, 255, 0), 1) { DashStyle = DashStyle.Dash };
        readonly Pen _penPendiente = new Pen(Color.FromArgb(140, 0, 255, 0), 1);
        readonly Pen _penRecorridoSel = new Pen(Color.FromArgb(220, 255, 138, 31), 1.5f) { DashStyle = DashStyle.Dash };
        readonly Pen _penPendienteSel = new Pen(Color.FromArgb(255, 255, 138, 31), 1.5f);
        readonly Pen _penAnillo = new Pen(Color.FromArgb(255, 138, 31), 1.5f);
        readonly Pen _penSeguridad = new Pen(Color.FromArgb(120, 255, 220, 0), 1) { DashStyle = DashStyle.Dash };

        // Silueta del avión mirando al norte, centrada en (0,0)
        static readonly PointF[] Silueta =
        {
            new PointF(0, -12),
            new PointF(1.6f, -8),  new PointF(1.8f, -3),
            new PointF(11, 4),     new PointF(11, 6.5f),
            new PointF(1.8f, 3),   new PointF(1.6f, 7.5f),
            new PointF(5, 10),     new PointF(5, 11.5f),
            new PointF(0, 10.5f),
            new PointF(-5, 11.5f), new PointF(-5, 10),
            new PointF(-1.6f, 7.5f), new PointF(-1.8f, 3),
            new PointF(-11, 6.5f), new PointF(-11, 4),
            new PointF(-1.8f, -3), new PointF(-1.6f, -8)
        };

        public RadarPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.Black;
            Size = new Size(Lado, Lado);
        }

        // ---------- Utilidades ----------
        public bool PosicionValida(double x, double y)
        {
            return x >= 0 && x < Lado && y >= 0 && y < Lado;
        }

        // Coordenada (0..599) -> píxel del panel. Origen arriba a la izquierda.
        PointF APantalla(Position p)
        {
            return new PointF((float)(p.GetX() * Width / Lado),
                              (float)(p.GetY() * Height / Lado));
        }

        // Rumbo en grados (0 = arriba, sentido horario). Público para usarlo desde el dashboard.
        public static double Rumbo(FlightPlan v)
        {
            Position a = v.GetinitialPosition(), b = v.GetfinalPosition();
            return Math.Atan2(b.GetX() - a.GetX(), -(b.GetY() - a.GetY())) * 180.0 / Math.PI;
        }

        // ---------- Clic: elegir avión ----------
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            FlightPlan elegido = null;
            float mejor = (float)RadioClic * RadioClic;          // distancia² máxima en píxeles

            foreach (FlightPlan v in Vuelos)
            {
                PointF c = APantalla(v.GetcurrentPosition());
                float dx = c.X - e.X, dy = c.Y - e.Y;
                float d2 = dx * dx + dy * dy;
                if (d2 <= mejor) { mejor = d2; elegido = v; }    // se queda con el más cercano
            }

            Seleccionado = elegido;                              // (repinta solo)
            VueloSeleccionado?.Invoke(this, elegido);
        }

        // ---------- Dibujo ----------
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            DibujarCuadricula(g);

            // 1. Trayectorias (el seleccionado se dibuja el último, encima)
            if (TrayectoriasTodas)
                foreach (FlightPlan v in Vuelos)
                    if (v != Seleccionado) DibujarTrayectoria(g, v, false);
            if (Seleccionado != null) DibujarTrayectoria(g, Seleccionado, true);

            // 2. Elipses de seguridad
            if (MostrarSeguridad)
                foreach (FlightPlan v in Vuelos)
                    DibujarSeguridad(g, APantalla(v.GetcurrentPosition()));

            // 3. Aviones
            foreach (FlightPlan v in Vuelos)
                if (v != Seleccionado)
                    DibujarAvion(g, APantalla(v.GetcurrentPosition()), Rumbo(v), _pincelAvion);

            // 4. El seleccionado, encima de todo, con anillo y su ID
            if (Seleccionado != null)
            {
                PointF c = APantalla(Seleccionado.GetcurrentPosition());
                DibujarAvion(g, c, Rumbo(Seleccionado), _pincelSel);
                g.DrawEllipse(_penAnillo, c.X - 17, c.Y - 17, 34, 34);
                g.DrawString("ID: " + Seleccionado.Getid(), _fuente, _pincelSel, c.X + 19, c.Y - 20);
                g.DrawString("Speed: " + Convert.ToString(Seleccionado.GetVelocidad()), _fuente, _pincelSel, c.X + 19, c.Y);
                g.DrawString("Posicion Actual: " + "(" + 
                    Convert.ToString(Math.Round(Seleccionado.GetcurrentPosition().GetX(), 2)) + " , " + 
                    Convert.ToString(Math.Round(Seleccionado.GetcurrentPosition().GetY(), 2)) + ")", _fuente, _pincelSel, c.X + 19, c.Y + 20);
                
            }
        }

        void DibujarCuadricula(Graphics g)
        {
            float ancho = Width - 1;
            float alto = Height - 1;

            using (var fina = new Pen(Color.FromArgb(70, ColorLineas), 1))
            using (var eje = new Pen(Color.FromArgb(160, ColorLineas), 1))
            using (var borde = new Pen(ColorLineas, 2))
            {
                for (int i = 1; i < Celdas; i++)
                {
                    Pen lapiz = (i == Celdas / 2) ? eje : fina;
                    float x = i * ancho / Celdas;
                    float y = i * alto / Celdas;
                    g.DrawLine(lapiz, x, 0, x, alto);
                    g.DrawLine(lapiz, 0, y, ancho, y);
                }
                g.DrawRectangle(borde, 1, 1, ancho - 2, alto - 2);
            }
        }

        // Guiones: lo ya recorrido. Línea continua: lo que le falta.
        void DibujarTrayectoria(Graphics g, FlightPlan v, bool seleccionado)
        {
            PointF ini = APantalla(v.GetinitialPosition());
            PointF act = APantalla(v.GetcurrentPosition());
            PointF fin = APantalla(v.GetfinalPosition());

            g.DrawLine(seleccionado ? _penRecorridoSel : _penRecorrido, ini, act);
            g.DrawLine(seleccionado ? _penPendienteSel : _penPendiente, act, fin);
        }

        void DibujarSeguridad(Graphics g, PointF centro)
        {
            float rx = (float)(DistanciaSeguridad * Width / Lado);
            float ry = (float)(DistanciaSeguridad * Height / Lado);
            g.DrawEllipse(_penSeguridad, centro.X - rx, centro.Y - ry, rx * 2, ry * 2);
        }

        void DibujarAvion(Graphics g, PointF centro, double rumboGrados, Brush relleno)
        {
            GraphicsState estado = g.Save();
            g.TranslateTransform(centro.X, centro.Y);
            g.RotateTransform((float)rumboGrados);
            g.ScaleTransform(EscalaAvion, EscalaAvion);
            g.FillPolygon(relleno, Silueta);
            g.Restore(estado);
        }
    }
}