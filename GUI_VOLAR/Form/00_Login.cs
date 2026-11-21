using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_VOLAR
{
    public partial class FrmLogin : Form
    {
        bool _pantallaCompleta;
        Rectangle _boundsNormales;
        int cont;
        public FrmLogin()
        {
            InitializeComponent();
            AlternarPantallaCompleta();
            cont = 0;
            FrmLabel1.Text = "Usuario";
            FrmLabel2.Text = "Contraseña";
            FrmButton.Text = "Iniciar Sesion";
        }

        private void FrmExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmMW_Click(object sender, EventArgs e)
        {
            AlternarPantallaCompleta();
        }

        private void AlternarPantallaCompleta() 
        {
            if (!_pantallaCompleta) 
            {
                _boundsNormales = WindowState == FormWindowState.Normal ? Bounds : RestoreBounds;

                WindowState = FormWindowState.Normal;
                FormBorderStyle = FormBorderStyle.None;
                Bounds = Screen.FromControl(this).Bounds;

                FrmMW.Image = Properties.Resources.icono_salir_pantalla_completa;
            }
            else 
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                this.ControlBox = false;
                WindowState = FormWindowState.Normal;

                FrmMW.Image = Properties.Resources.icono_pantalla_completa;
            }

            _pantallaCompleta =! _pantallaCompleta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(cont == 0) 
            {

            }
            if (cont == 1) 
            {
                //Comparamos que el codigo es correcto....
                //si la condicion es correcta:
                FrmLabel1.Text = "Nuevo Usuario";
                FrmLabel2.Text = "Crear Contraseña";
                FrmButton.Text = "Crear Sesion";

                FrmLabel2.Visible = true;
                FrmTextBox2.Visible = true;

                cont = 0;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cont == 0) 
            {
                FrmLabel1.Text = "Correo Electronico";
                FrmLabel2.Visible = false;
                FrmTextBox2.Visible = false;
                FrmButton.Text = "Mandar Codigo de Activacion";
                cont = 1;
            }
        }
    }
}
