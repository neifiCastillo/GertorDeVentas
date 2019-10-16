using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SisVenttas.Datos;
using SistemaVentas.Datos;
using SistemaVentas.Entidades;


namespace SistemaVentas.Presentacion
{
    public partial class FrmClientes : Form
    {
        private static DataTable dt = new DataTable();
        public FrmClientes()
        {
            InitializeComponent();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = FCliente.GetAll();
                dt = ds.Tables[0];
                GridCliente.DataSource = dt;

                if(dt.Rows.Count > 0)
                {
                    LblDatosNoEncontrados.Visible = false;
                    GridCliente_CellClick(null, null);

                }
                else
                {
                    LblDatosNoEncontrados.Visible = true;

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

            MostrarGuardarCancelar(false);

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
                        Cliente cliente = new Cliente();
                        cliente.Nombre = txtNombre.Text;
                        cliente.Apellido = txtApellido.Text;
                        cliente.Direccion = txtDireccion.Text;
                        cliente.Dni = Convert.ToInt32(txtDni.Text);
                        cliente.Telefono = txtTelefono.Text;

                        if (FCliente.Insertar(cliente) > 0)
                        {
                            MessageBox.Show("Datos Guardados Correctamente");
                            FrmClientes_Load(null, null);
                        }

                    }
                    else
                    {
                        Cliente cliente = new Cliente();
                        cliente.Id = Convert.ToInt32(lbllid.Text);
                        cliente.Nombre = txtNombre.Text;
                        cliente.Apellido = txtApellido.Text;
                        cliente.Direccion = txtDireccion.Text;
                        cliente.Dni = Convert.ToInt32(txtDni.Text);
                        cliente.Telefono = txtTelefono.Text;

                        if (FCliente.Actualizar(cliente) == 1)
                        {
                            MessageBox.Show("Datos Modificados Correctamente");
                            FrmClientes_Load(null, null);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Faltan Cargar datos:\n"+ sResultado);

                }

               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }
            
        }

        internal void SetFlag(string Band)
        {
            txtFlag.Text = Band;
        }

        public string ValidarDatos()
        {
            string Resultado = "";
            if (txtNombre.Text == "")
            {
                Resultado = Resultado + "Nombre \n";
            }
            if (txtApellido.Text == "")
            {
                Resultado = Resultado + "Apellido \n";
            }

            return Resultado;
        }
        public void MostrarGuardarCancelar(bool b)
        {
            BtnGuardar.Visible = b;
            BtnCancelar.Visible = b;
            BtnNuevo.Visible = !b;
            BtnEditar.Visible = !b;
            BtnEliminar.Visible = !b;
            GridCliente.Enabled = !b;

            txtNombre.Enabled = b;
            txtApellido.Enabled = b;
            txtDni.Enabled = b;
            txtDireccion.Enabled = b;
            txtTelefono.Enabled = b;
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDni.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtBuscar.Text = "";
            lbllid.Text = "0";
         

        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);


        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(false);
            GridCliente_CellClick(null, null);
        }

        private void GridCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (GridCliente.CurrentRow != null)
            {
                lbllid.Text = GridCliente.CurrentRow.Cells[1].Value.ToString();
                txtNombre.Text = GridCliente.CurrentRow.Cells[2].Value.ToString();
                txtApellido.Text = GridCliente.CurrentRow.Cells[3].Value.ToString();
                txtDni.Text = GridCliente.CurrentRow.Cells[4].Value.ToString();
                txtDireccion.Text = GridCliente.CurrentRow.Cells[5].Value.ToString();
                txtTelefono.Text = GridCliente.CurrentRow.Cells[6].Value.ToString();

            }
        }

        private void GridCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == GridCliente.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)GridCliente.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);

            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("¿Desea Eliminar los Clientes Seleccionados?" , "Eliminacion de Clientes", MessageBoxButtons.OKCancel , MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in GridCliente.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Eliminar"].Value))
                        {
                            Cliente cliente = new Cliente();
                            cliente.Id = Convert.ToInt32(row.Cells["Id"].Value);
                            if (FCliente.Eliminar(cliente) != 1)
                            {
                                MessageBox.Show("el cliente no pudo ser eliminada", "Eliminacion de cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                        }

                    }
                    FrmClientes_Load(null, null);

                }


            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
                    
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(dt.Copy());
                dv.RowFilter = CmbBuscar.Text + " LIKE '" +  txtBuscar.Text  +  "%'";

                GridCliente.DataSource = dv;
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

        private void GridCliente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (txtFlag.Text == "1")
            {


                FrmVenta frmVentas = FrmVenta.GetInscance();

                if (GridCliente.CurrentRow != null)
                {
                    frmVentas.SetCliente(GridCliente.CurrentRow.Cells[1].Value.ToString(), GridCliente.CurrentRow.Cells[2].Value.ToString());
                    frmVentas.Show();
                    Close();


                }
            }

        }
    }
}
