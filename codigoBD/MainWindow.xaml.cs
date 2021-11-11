using System;
using System.Collections.Generic;
using System.Data.SqlClient; //<==
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace codigoBD
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Usuario> listaUsuarios = new List<Usuario>();
        public MainWindow()
        {
            InitializeComponent();
            //Coffe hasta 11:45
            
            //puedo obtener la cadena de conexión a traves de la conexión hecha por VS
            string cadena = "Data Source=LAPTOP-PC8SL5H1;Initial Catalog=registrosUsuario;User ID=sa;Password=123456";

            string servidor = "LAPTOP-PC8SL5H1";
            string bd = "registrosUsuario";

            //puedo obetner la cadena de conxión, creandola con la ayuda del objeto SqlConnectionStringBuilder
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = servidor;
            scsb.InitialCatalog = bd;
            scsb.UserID = "sa";
            scsb.Password = "123456";

            Debug.WriteLine(cadena);
            Debug.WriteLine(scsb.ConnectionString);

            //Hago la xonexión a través del objeto SqlConnection
            SqlConnection sqlc = new SqlConnection(scsb.ConnectionString);
            sqlc.Open();

            string consulta = "select * from usuario;";

            //acá dejo listo el comando a enviar, que lleva la consulta y el objeto SqlConnection
            SqlCommand comando = new SqlCommand(consulta, sqlc);
            SqlDataReader response = comando.ExecuteReader();//Aquí estoy ejecutando la consulta en la base de datos.
            Usuario u = null;
            while (response.Read())
            {
                int id = response.GetInt32(0);
                string nombre = response.GetString(1);
                string pass = response.GetString(2);
                u = new Usuario(id, nombre, pass);
                listaUsuarios.Add(u);
            }

            foreach(Usuario us in listaUsuarios)
            {
                Debug.WriteLine(us.Id + " " + us.Nombre + " " + us.Pass);
            }
            
        }
    }
}
