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
        public int Celdas { get; set; } = 12; // 12 celdas = 50 unidades cada una

        Color _colorLineas = Color.FromArgb(0, 200, 0);
        public Color ColorLineas
        {
            get { return _colorLineas; }
            set { _colorLineas = value; Invalidate(); }
        }

        // ---------- Aviones ----------
        // Los atributos evitan que el diseñador intente guardar la lista (daría error)
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<FlightPlan> Vuelos { get; } = new List<FlightPlan>();

        public float EscalaAvion { get; set; } = 1.0f;     // 1 = unos 22 x 24 px

        // Se crea una sola vez, no en cada repintado
        readonly Brush _pincelAvion = new SolidBrush(Color.FromArgb(0, 255, 0));

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
        // Comprueba que una coordenada cabe en el radar
        public bool PosicionValida(double x, double y)
        {
            return x >= 0 && x < Lado && y >= 0 && y < Lado;
        }

        // Coordenada (0..599) -> píxel del panel. Origen arriba a la izquierda, Y hacia abajo.
        // Al multiplicar por Width / Lado sigue funcionando si Windows escala el programa.
        PointF APantalla(Position p)
        {
            return new PointF((float)(p.GetX() * Width / Lado),
                              (float)(p.GetY() * Height / Lado));
        }

        // Rumbo en grados (0 = norte/arriba, sentido horario).
        // Se invierte dy porque en pantalla el norte es "Y menor".
        static double Rumbo(FlightPlan v)
        {
            Position a = v.GetinitialPosition(), b = v.GetfinalPosition();
            return Math.Atan2(b.GetX() - a.GetX(), -(b.GetY() - a.GetY())) * 180.0 / Math.PI;
        }

        // ---------- Dibujo ----------
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            DibujarCuadricula(g);

            foreach (FlightPlan v in Vuelos)
                DibujarAvion(g, APantalla(v.GetcurrentPosition()), Rumbo(v));
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

        void DibujarAvion(Graphics g, PointF centro, double rumboGrados)
        {
            GraphicsState estado = g.Save();               // guarda cómo está el lienzo
            g.TranslateTransform(centro.X, centro.Y);      // 1. el (0,0) pasa a ser el avión
            g.RotateTransform((float)rumboGrados);         // 2. gira al rumbo
            g.ScaleTransform(EscalaAvion, EscalaAvion);    // 3. escala
            g.FillPolygon(_pincelAvion, Silueta);          // 4. pinta el avión
            g.Restore(estado);                             // deja el lienzo como estaba
        }
    }
}