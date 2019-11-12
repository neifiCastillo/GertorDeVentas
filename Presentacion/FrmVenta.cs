using SistemaVentas.Datos;
using SistemaVentas.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion
{

    public partial class FrmVenta : Form
    {

        private static DataTable dt = new DataTable();
        private static FrmVenta _instancia = null;
        public FrmVenta()
        {
            InitializeComponent();
        }
        public static FrmVenta GetInscance()
        {
            if (_instancia == null)
                _instancia = new FrmVenta();
            return _instancia;
        }

        private void FrmVenta_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = FVenta.GetAll();
                dt = ds.Tables[0];
                GridVentas.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    LblDatosNoEncontrados.Visible = false;
                    GridVentas_CellClick(null, null);

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

            MostrarGuardarCancelar(false);

        }
        public void MostrarGuardarCancelar(bool b)
        {
            BtnGuardar.Visible = b;
            BtnCancelar.Visible = b;
            BtnBuscarCliente.Visible = b;
            BtnNuevo.Visible = !b;
            BtnEditar.Visible = !b;
           
            GridVentas.Enabled = !b;
            txtFecha.Enabled = b;
            txtNumeroDocumento.Enabled= b;
            cmbTipoDoc.Enabled = b;

            
        }

        private void BtnBuscarCliente_Click(object sender, EventArgs e)
        {
            FrmClientes frmccli = new FrmClientes();
            frmccli.SetFlag("1");
            frmccli.ShowDialog();
        }
        public string ValidarDatos()
        {
            string Resultado = "";
            if (txtClienteId.Text == "")
            {
                Resultado = Resultado + "Cliente \n";
            }
            if (txtNumeroDocumento.Text == "")
            {
                Resultado = Resultado + "Numero de Documento \n";
            }

            return Resultado;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                string sResultado = ValidarDatos();
                if (sResultado == "")
                {

                    if (lbllid.Text == "0")
                    {
                        Venta venta = new Venta();
                        venta.Cliente.Id = Convert.ToInt32(txtClienteId.Text);
                        venta.FechaVenta = txtFecha.Value;
                        venta.TipoDocumento = cmbTipoDoc.Text;
                        venta.NumeroDocumento = txtNumeroDocumento.Text;

                        venta.Cliente.Nombre = txtClienteNombre.Text;

                        int iVentaId = FVenta.Insertar(venta);

                        if (iVentaId > 0)
                        {
                            FrmVenta_Load(null, null);
                            venta.Id = iVentaId;
                            CargarDetalle(venta);
                        }

                    }
                    else
                    {

                        Venta venta = new Venta();
                        venta.Id = Convert.ToInt32(lbllid.Text);
                        venta.Cliente.Id = Convert.ToInt32(txtClienteId.Text);
                        venta.FechaVenta = txtFecha.Value;
                        venta.TipoDocumento = cmbTipoDoc.Text;
                        venta.NumeroDocumento = txtNumeroDocumento.Text;


                        if (FVenta.Actualizar(venta) == 1)
                        {
                            MessageBox.Show("Datos Modificados Correctamente");
                            FrmVenta_Load(null, null);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Faltan Cargar datos:\n" + sResultado);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }


        }

        private void CargarDetalle(Venta venta)
        {
            FrmDetalleVenta fDetVenta =  FrmDetalleVenta.GetInstance();
            fDetVenta.Setventa(venta);
            fDetVenta.ShowDialog();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(false);
            GridVentas_CellClick(null, null);

        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
            txtClienteId.Text = "";
            txtClienteNombre.Text = "";
            txtNumeroDocumento.Text = "";
            txtBuscar.Text = "";
            lbllid.Text = "0";

        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(dt.Copy());
                dv.RowFilter = CmbBuscar.Text + " LIKE '" + txtBuscar.Text + "%'";

                GridVentas.DataSource = dv;
                if (dv.Count == 0)
                {
                    LblDatosNoEncontrados.Visible = true;
                }
                else
                {
                    LblDatosNoEncontrados.Visible = false;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }

        }

        private void GridVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

           // if (e.ColumnIndex == GridVentas.Columns["Eliminar"].Index)
           // {
           //     DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)GridVentas.Rows[e.RowIndex].Cells["Eliminar"];
           //     chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);

            //}

        }

        private void GridVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridVentas.CurrentRow != null)
            {
                 
                lbllid.Text = GridVentas.CurrentRow.Cells["Id"].Value.ToString();
                txtClienteId.Text = GridVentas.CurrentRow.Cells["ClienteId"].Value.ToString();
                txtClienteNombre.Text = GridVentas.CurrentRow.Cells["Nombre"].Value.ToString() + "" + GridVentas.CurrentRow.Cells["Apellido"].Value.ToString();
                txtFecha.Text = GridVentas.CurrentRow.Cells["FechaVenta"].Value.ToString();
                cmbTipoDoc.Text = GridVentas.CurrentRow.Cells["TipoDocumento"].Value.ToString();
                txtNumeroDocumento.Text = GridVentas.CurrentRow.Cells["NumeroDocumento"].Value.ToString();

            }

        }

        internal void SetCliente(string sIdCliente, string sNombreCliente)
        {
            txtClienteId.Text = sIdCliente;
            txtClienteNombre.Text = sNombreCliente;
        }

        private void GridVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridVentas.CurrentRow != null)
            {
                Venta venta = new Venta();

                venta.Id=Convert.ToInt32(GridVentas.CurrentRow.Cells["Id"].Value.ToString());
                venta.Cliente.Id= Convert.ToInt32(GridVentas.CurrentRow.Cells["ClienteId"].Value.ToString());
                venta.Cliente.Nombre= GridVentas.CurrentRow.Cells["Nombre"].Value.ToString() + "" + GridVentas.CurrentRow.Cells["Apellido"].Value.ToString();
                venta.FechaVenta = Convert.ToDateTime(GridVentas.CurrentRow.Cells["FechaVenta"].Value.ToString());
                venta.TipoDocumento= GridVentas.CurrentRow.Cells["TipoDocumento"].Value.ToString();
                venta.NumeroDocumento= GridVentas.CurrentRow.Cells["NumeroDocumento"].Value.ToString();

                CargarDetalle(venta);
        

                }

        }
    }
}
