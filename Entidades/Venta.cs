using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades
{
   public class Venta
    {

        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime FechaVenta { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        
        public Venta()
        {
            Cliente = new Cliente();
        }

    }
}
