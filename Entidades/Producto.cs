using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public Categoria Categoria { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Stock { get; set; }
        public double PrecioCompra { get; set; }
        public double PrecioVenta { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public byte[] Imagen { get; set; }

        public Producto()
        {
            Categoria = new Categoria();
        }
        public Producto (int id , Categoria categoria, string nombre , string descripcion , double stock ,double precioCompra, double precioventa,DateTime fechavencimiento , byte [] imagen)
        {
            this.Id = id;
            Categoria = categoria;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Stock = stock;
            this.PrecioCompra = precioCompra;
            this.PrecioVenta = precioventa;
            this.FechaVencimiento = fechavencimiento;
            this.Imagen = imagen;


        }
    }
}
