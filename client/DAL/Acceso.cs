using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Collections;
using System.Data;

namespace DAL
{
    public class Acceso
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MGE6652\\SQLEXPRESS;Initial Catalog=trabajo_practico;Integrated Security=True");

        public void Close()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
                GC.Collect();
            }
        }
        public bool Execute(string arg, Dictionary<string, object> parameters)
        {
            conn.Open();
            int filas = 0;
            using (SqlCommand cmd = new SqlCommand(arg, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (KeyValuePair<string, object> kvp in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                    }
                    filas = cmd.ExecuteNonQuery();
                }
            }
            Close();
            return filas > 0;
        }


        // reader
        public DataTable Read(string arg, Dictionary<string, object> parameters = null)
        {
            conn.Open();
            DataTable db = new DataTable();
            var cmd = new SqlCommand(arg, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (KeyValuePair<string, object> kvp in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                    }
                }
                da.Fill(db);
                return db;
            } catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
                cmd.Dispose();
            }
        }
    }
}
