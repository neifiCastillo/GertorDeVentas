using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades
{
    public class DetalleVenta
    {

        public int Id { get; set; }
        public Venta Venta { get; set; }
        public Producto Producto { get; set; }
        public double Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
             
        public DetalleVenta()
        {
            Venta = new Venta();
            Producto = new Producto();
        }




    }
}
