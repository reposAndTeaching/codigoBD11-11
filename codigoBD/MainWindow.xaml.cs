using codigoBD.modelBD;//<---
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
        private Data d;
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
            sqlc.Open(); // <---------------------------

            string consulta = "select * from usuario;";

            //acá dejo listo el comando a enviar, que lleva la consulta y el objeto SqlConnection
            SqlCommand comando = new SqlCommand(consulta, sqlc);
            SqlDataReader response = comando.ExecuteReader();//Aquí estoy ejecutando la consulta en la base de datos.

            Usuario u = null;

            // => response

            while (response.Read())
            {
                int id = response.GetInt32(0);
                string nombre = response.GetString(1);
                string passs = response.GetString(2);
                var mayor = response.GetValue(3);

                Debug.WriteLine(mayor);
                Debug.WriteLine(mayor.GetType());


                //u = new Usuario(id, nombre, passs, mayor);
                //listaUsuarios.Add(u);
            }

            sqlc.Close();//debemos cerrar la conexión

            //listaUsuarios => Va a tener todos (depende de la consulta)
            //los registros de la base de datos correspondiente a la tabla usuario

            foreach(Usuario us in listaUsuarios)
            {
                Debug.WriteLine(us.Id + " " + us.Nombre + " " + us.Contrasenia);
            }

            //Acá comienzo el 18/11
            Debug.WriteLine("================================================");
            servidor = "LAPTOP-PC8SL5H1";
            bd = "registrosUsuario";
            string userid = "sa";
            string pass = "123456";
            d = new Data(servidor, bd, userid, pass);
            ActualizarDataGrid();
            
            foreach(Usuario user in d.GetUsuarios())
            {
                Debug.WriteLine($"[{user.Id}]-Usuario: {user.Nombre} Password: {user.Contrasenia}");
            }
            Console.WriteLine("***");

            Usuario miUsuario = d.GetUsuario(1);
            Debug.WriteLine(miUsuario.Id);
            Debug.WriteLine(miUsuario.Nombre);
            Debug.WriteLine(miUsuario.Contrasenia);

            
        }

        private void buttonRegistrar_Click(object sender, RoutedEventArgs e)
        {
            string nombreUsuario = textBoxNombre.Text;
            string passUsuario = passwordBoxPass.Password;
            bool esMayor = (bool)checkBoxMayor.IsChecked;
            

            Usuario nuevoUsuario = new Usuario(nombreUsuario, passUsuario, esMayor);
            try
            {
                d.AddUsuario(nuevoUsuario);

                MessageBox.Show(
                "Se ha registrado el usuario " + nombreUsuario + "!",
                "Nuevo Registro",
                MessageBoxButton.OK,
                MessageBoxImage.Information
                );

                
                textBoxNombre.Text = "";
                passwordBoxPass.Clear();
                textBoxNombre.Focus();
            
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("Se ha generado una excepción");
                Debug.WriteLine(ex.Message);
            }

            

            

        }

        private void buttonDeCaja_Click(object sender, RoutedEventArgs e)
        {
            //Clase que nos permite crear objetos caja de mensajes.
            MessageBoxResult respuesta = MessageBox.Show("Desea usted ir a la otra ventana?", "Abrir Nueva Ventana", MessageBoxButton.YesNo, MessageBoxImage.Question);

            switch (respuesta)
            {
                case MessageBoxResult.Yes:
                    NuevaVentana nv = new NuevaVentana();
                    nv.Show();

                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Ha presionado el botón No");
                    break;
            }
        }

        private void ActualizarDataGrid()
        {
            dataGridUsuarios.ItemsSource = d.GetUsuarios();
        }
    }
}
