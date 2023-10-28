using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioDAL
    {
        int[] Partidas = new int[3];
        string email;
        string username;

        public int[] PartidasJugadas
        {
            get { return Partidas; }
        }
        public string Email { get { return email; } }
        public string Username { get { return username; } }
        public void BuscarUsuario(Dictionary<string, object> args)
        {
            var ac = new Acceso();
            DataTable db = ac.Read("BuscarUsuarioI", args);
            if (db != null && db.Rows.Count > 0)
            {
                DataRow r = db.Rows[0];
                this.email = r["email"].ToString();
                this.username = r["username"].ToString();

                foreach (DataRow dr in db.Rows)
                {
                    if (Int32.Parse(dr["FINISH_COND"].ToString()) == 0)
                        this.Partidas[0]++;
                    if (Int32.Parse(dr["FINISH_COND"].ToString()) == 1)
                        this.Partidas[1]++;
                    if (Int32.Parse(dr["FINISH_COND"].ToString()) == 2)
                        this.Partidas[2]++;
                }
            } else
            {
                throw new Exception("Ese usuario no existe");
            }
        }

        public void RegistrarUsuario(Dictionary<string, object> args)
        {
            var ac = new Acceso();
            ac.Execute("RegistrarUsuario", args);
        }
    
       

    }
}
