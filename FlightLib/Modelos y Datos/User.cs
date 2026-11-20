using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib.Modelos_y_Datos
{
    public  class User
    {
        public string Email {  get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

    }
}
