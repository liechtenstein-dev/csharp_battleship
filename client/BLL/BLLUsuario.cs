using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLUsuario
    {
        private DALUsuario dalusuario = new DALUsuario();
        public bool BuscarUsuario(string email, string password)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Email", email },
                { "@Password", password }
            };
            if (dalusuario.BuscarUsuario(parameters) == 0)
                return false;
            return true;
        }   

        public void RegistrarUsuario(string email, string password, string username)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Email", email },
                { "@Password", password },
                { "@Username", username }
            };
            dalusuario.RegistrarUsuario(parameters);
        }

    }
}
