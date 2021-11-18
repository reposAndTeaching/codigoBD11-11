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
        bool esMayorDeEdad;

        public Usuario(int id, string nombre, string pass, bool esMayorDeEdad)
        {
            this.id = id;
            this.nombre = nombre;
            this.pass = pass;
            this.esMayorDeEdad = esMayorDeEdad;
        }

        public Usuario(string nombre, string pass, bool esMayorDeEdad)
        {
            this.nombre = nombre;
            this.pass = pass;
            this.esMayorDeEdad = esMayorDeEdad;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Contrasenia { get => pass; set => pass = value; }
        public bool EsMayorDeEdad { get => esMayorDeEdad; set => esMayorDeEdad = value; }
    }
}
