using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALUsuario
    {
        string email;
        string username;

        public int BuscarUsuario(Dictionary<string, object> args)
        {
            var ac = new Acceso();
            DataTable db = ac.Read("BuscarUsuarioI", args);
            object primerValor = db.Rows[0]["existe"];
            int existe = 0;
            if (primerValor != DBNull.Value)
            {
               existe = Convert.ToInt32(primerValor);
            }
            return existe;
        }

        public void RegistrarUsuario(Dictionary<string, object> args)
        { 
            var ac = new Acceso();
            ac.Execute("RegistrarUsuario", args);
        }

    }
}
