using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codigoBD
{
    class Usuario
    {
        int id;
        string nombre;
        string pass;

        public Usuario(int id, string nombre, string pass)
        {
            this.id = id;
            this.nombre = nombre;
            this.pass = pass;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Pass { get => pass; set => pass = value; }
    }
}
