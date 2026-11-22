using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GUI_VOLAR
{
    public class RadarPanel : Panel
    {
        public int Celdas { get; set; } = 12;
        public Color ColorLineas { get; set; } = Color.FromArgb(0, 200, 0);

        public RadarPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.Black;
            Size = new Size(600, 600);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

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
    }
}
