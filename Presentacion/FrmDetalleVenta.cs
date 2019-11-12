using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Datos;
using SistemaVentas.Entidades;

namespace SistemaVentas.Presentacion
{
    public partial class FrmDetalleVenta : Form
    {
        private static DataTable dt = new DataTable();
        private static FrmDetalleVenta _instancia = null;
        public FrmDetalleVenta()
        {
            InitializeComponent();
        }
        public static FrmDetalleVenta GetInstance()
        {
            if (_instancia == null)
           
                _instancia = new FrmDetalleVenta();

                return _instancia;
            
        }

        private void BtnBuscarProducto_Click(object sender, EventArgs e)
        {
            FrmProducto frmProd = FrmProducto.GetInscance();
            frmProd.SetFlag("1");
            frmProd.ShowDialog();
        }

        internal void SetProducto(Producto producto)
        {

            txtProductoId.Text = producto.Id.ToString();
            txtProductoDescripcion.Text = producto.Nombre;
            txtStock.Text = producto.Stock.ToString();
            txtPrecioUnitario.Text = producto.PrecioVenta.ToString();


        }

        internal void Setventa(Venta venta)
        {
            
            txtVentaId.Text = venta.Cliente.Id.ToString();
            txtClienteNombre.Text = venta.Cliente.Nombre;
            txtFecha.Text = venta.FechaVenta.ToShortDateString();
            cmbTipoDoc.Text = venta.TipoDocumento;
            txtNumeroDocumento.Text = venta.NumeroDocumento;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = ValidarDatos();
                if (sResultado == "")
                {

                        DetalleVenta detVenta = new DetalleVenta();
                        detVenta.Venta.Id = Convert.ToInt32(txtVentaId.Text);
                        detVenta.Producto.Id = Convert.ToInt32(txtProductoId.Text);
                        detVenta.Cantidad = Convert.ToDouble(txtCantidad.Text);
                        detVenta.PrecioUnitario = Convert.ToDouble(txtPrecioUnitario.Text);


                        int iDetVentaId =FDetalleVenta.Insertar(detVenta);

                        if (iDetVentaId > 0)
                        {
                        FDetalleVenta.DisminuirStock(detVenta);
                            MessageBox.Show("Producto registrado correctamente!");
                            FrmDetalleVenta_Load(null, null);
                            Limpiar();
                        }
                        else
                        {
                            MessageBox.Show("Error al registrar el producto , intente nuevamente");
                        }
                    
                   
                }
                else
                {
                    MessageBox.Show(sResultado,"Error",MessageBoxButtons.OK,MessageBoxIcon.Information);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }

        }

        private void Limpiar()
        {
            txtProductoId.Text = "";
            txtProductoDescripcion.Text = "";
            txtCantidad.Text = "1";
            txtStock.Text = "0";
            txtPrecioUnitario.Text = "";
        }

        private string ValidarDatos()
        {
            string Resultado = "";
            if (txtProductoId.Text == "")
            {
                Resultado = Resultado + "Debe seleccionar un producto";
            }
            if (Convert.ToInt32(txtCantidad.Text) > Convert.ToInt32(txtStock.Text) )
            {
                Resultado = Resultado + "La cantidad que intenta vender supera el stock\n";
            }

            return Resultado;
        }

        private void FrmDetalleVenta_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = FDetalleVenta.GetAll(Convert.ToInt32(txtVentaId.Text));
                dt = ds.Tables[0];
                GridVentas.DataSource = dt;
                GridVentas.Columns["VentaId"].Visible = false;
                GridVentas.Columns["Id"].Visible = false;
                GridVentas.Columns["ProductoId"].Visible = false;
                GridVentas.Columns["PrecioVenta"].Visible = false;

                if (dt.Rows.Count > 0)
                {
                    LblDatosNoEncontrados.Visible = false;
                    //GridVentas_CellClick(null, null);

                }
                else
                {
                    LblDatosNoEncontrados.Visible = true;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

           // MostrarGuardarCancelar(false);

        }

        private void GridVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == GridVentas.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)GridVentas.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);

            }
        }

        private void BtnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea Quitar los Productos Seleccionados?", "Eliminacion de Producto", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in GridVentas.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Eliminar"].Value))
                        {
                            DetalleVenta detVenta = new DetalleVenta();
                            detVenta.Producto.Id= Convert.ToInt32(row.Cells["ProductoId"].Value);
                            detVenta.Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                            detVenta.Id = Convert.ToInt32(row.Cells["Id"].Value);
                            if(FDetalleVenta.Eliminar(detVenta)> 0)
                            { 
                            if (FDetalleVenta.AumentarStock(detVenta) != 1)
                            {
                                MessageBox.Show("No se pudo actulizar el Stock", "Eliminacion de Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }


                            }
                            else
                            {
                                MessageBox.Show("el producto no pudo ser Quitado", "Eliminacion de Producto. Intente de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                        }

                    }
                    FrmDetalleVenta_Load(null, null);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }
        }
    }
}
