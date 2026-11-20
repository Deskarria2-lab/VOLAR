using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Clase encargada para el servicio de Login, Registrar usuarios, etc

namespace FlightLib.Base_de_Datos
{
    internal class AuthService
    {
        private readonly UserRepository repository;

        public AuthService(UserRepository repository) 
        {
            this.repository = repository;
        }

        public bool EmailExists(string email)
        {
            return repository.GetByEmail(email) != null;
        }
    }
}
