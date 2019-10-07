using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Direccion { get; set; }
        public int Dni { get; set; }
        public string Telefono { get; set; }
        public Cliente()
        {

        }
        public Cliente(int id , string nombre , string apellido , string direccion , int dni , string telefono)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Direccion = direccion;
            this.Dni = dni;
            this.Telefono = telefono;
        }






    }
}
