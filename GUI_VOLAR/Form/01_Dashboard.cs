using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_VOLAR
{
    public partial class FrmDashboard : Form
    {
        FrmLogin login;
        public FrmDashboard(FrmLogin login)
        {
            InitializeComponent();
            this.login = login;
        }
    }
}
