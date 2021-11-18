using System;
using System.Collections.Generic;
using System.Data.SqlClient; //Objeto SqlConnectionStringBuilder
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codigoBD
{
    class Conexion
    {
        private string servidor; //DataSource
        private string bd; //InitialCatalog
        private string userid; //UserID
        private string pass; //Password

        private bool windowsAuth; //Integrated Security

        private SqlConnectionStringBuilder csb;

        public Conexion(string servidor, string bd, string userid, string pass)
        {//Con este constructor, puedo utilizar autenticación SQL
            csb = new SqlConnectionStringBuilder();
            csb.DataSource = servidor;
            csb.InitialCatalog = bd;
            csb.UserID = userid;
            csb.Password = pass;
        }

        public Conexion(string servidor, string bd, bool windowsAuth)
        {//Con este constructor, puedo utilizar autenticación de windows
            csb = new SqlConnectionStringBuilder();
            csb.DataSource = servidor;
            csb.InitialCatalog = bd;
            csb.IntegratedSecurity = windowsAuth;
        }

        public SqlConnectionStringBuilder Csb { get => csb;}
    }
}
