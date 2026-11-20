using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Clase para definir los elementos del usuario
namespace FlightLib.Base_de_Datos
{
    public class User
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
