using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pruebas_GUI_Volar
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////    Clase para gestionar el menu de Pop ups alerta  /////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class AvisosBox : Form
    {
        ///////////////////////////////////////////////////////////     Constructor     ///////////////////////////////////////////////////
        public AvisosBox(string mensaje)                                                //  El constructor pide el mensaje para mostrar 
        {                                                                               //
            InitializeComponent();                                                      //  Iniciamos los componentes, esto siempre esta;
            av_text.Text = mensaje;                                                     //  El texto en el Label es el introducido
        }                                                                               //
    }                                                                                   //
}
