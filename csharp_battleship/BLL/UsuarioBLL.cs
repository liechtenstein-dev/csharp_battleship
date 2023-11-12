using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioBLL
    {
        public int[] Partidas = new int[3];
        public string email;
        public string username;
        public string password;
        public UsuarioBLL(string email, string password)
        { 
            this.email = email;
            this.password = password;
        }
        public UsuarioBLL(string email, string password, string username) : this(email, password){ 
            this.username = username;
        }

        public void BuscarUsuario()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Email", this.email },
                { "@Password", this.password }
            };
            var u = new UsuarioDAL();
            u.BuscarUsuario(parameters);
            this.Partidas = u.PartidasJugadas;
            this.email = u.Email;
            this.username = u.Username;
        }

        public void RegistrarUsuario()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Email", this.email },
                { "@Password", this.password },
                { "@Username", this.username }
            };

            var u = new UsuarioDAL();
            u.RegistrarUsuario(parameters);
        }

    }
}
