using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Esta clase se encarga del login

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
