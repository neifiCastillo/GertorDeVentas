using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas
{
    public partial class FrmReporteVenta : Form
    {
        public FrmReporteVenta()
        {
            InitializeComponent();
        }
        public void SetVentaId(int ventaId)
        {
            txtVentaId.Text = ventaId.ToString();

        }

        private void FrmReporteVenta_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetPrincipal.usp_Reportes_GenerarReporteVenta' Puede moverla o quitarla según sea necesario.
            this.usp_Reportes_GenerarReporteVentaTableAdapter.Fill(this.DataSetPrincipal.usp_Reportes_GenerarReporteVenta, Convert.ToInt32(txtVentaId.Text));

            this.reportViewer1.RefreshReport();
        }
    }
}
