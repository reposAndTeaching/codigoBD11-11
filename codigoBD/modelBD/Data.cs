using System;
using System.Collections.Generic;
using System.Data.SqlClient;//<-- SQL
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codigoBD.modelBD
{
    class Data
    {
        private Conexion con;
        private SqlConnection sqlcon;
        private SqlCommand sqlcmd;
        private SqlDataReader response;

        private string consulta;

        public Data(string servidor, string bd, string user, string pass)
        {
            con = new Conexion(servidor, bd, user, pass);
        }

        //Traerá todos los usuarios de la tabla "usuario"
        public List<Usuario> GetUsuarios()
        {
            List<Usuario> listaUsuario = new List<Usuario>();
            Usuario u = null;
            consulta = "select * from usuario;";

            using(sqlcon = new SqlConnection(con.Csb.ConnectionString))
            {
                sqlcon.Open();
                //Toda la lógica de la consulta
                using(sqlcmd = new SqlCommand(consulta, sqlcon))
                {
                    response = sqlcmd.ExecuteReader();

                    while (response.Read())
                    {
                        int id = response.GetInt32(0);
                        string nombre = response.GetString(1);
                        string passs = response.GetString(2);
                        bool mayor = response.GetBoolean(3);

                        u = new Usuario(id, nombre, passs, mayor);
                        listaUsuario.Add(u);
                    }

                }
            }//Cerrará la conexión automáticamente // implícitamente hay un sqlcon.Close();
            return listaUsuario;
        }

        public Usuario GetUsuario(int id)
        {
            Usuario u = null;
            consulta = $"select * from usuario where id = {id};";
            using(sqlcon = new SqlConnection(con.Csb.ConnectionString))
            {
                sqlcon.Open();
                using (sqlcmd = new SqlCommand(consulta, sqlcon))
                {
                    response = sqlcmd.ExecuteReader();

                    if (response.Read()) //pero el while va a funcionar igual
                    {
                        int _id = response.GetInt32(0);
                        string nombre = response.GetString(1);
                        string passs = response.GetString(2);
                        bool mayor = response.GetBoolean(3);

                        u = new Usuario(_id, nombre, passs, mayor);
                    }
                }
            }

            return u;
        }

        public int AddUsuario(Usuario u)
        {
            int filasAfectadas;
            consulta = $"insert into usuario values('{u.Nombre}', '{u.Contrasenia}', {u.EsMayorDeEdad});";
            using (sqlcon = new SqlConnection(con.Csb.ConnectionString))
            {
                sqlcon.Open();
                using (sqlcmd = new SqlCommand(consulta, sqlcon))
                {
                    filasAfectadas = sqlcmd.ExecuteNonQuery();
                }
            }
            return filasAfectadas;
        }
    }
}
