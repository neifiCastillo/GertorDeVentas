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
    public partial class FrmCategoria : Form
    {
        private static DataTable dt = new DataTable();
        public FrmCategoria()
        {
            InitializeComponent();
        }

        public void SetFlag(string valor)
        {
            txtFlag.Text = valor;
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = FCategoria.GetAll();
                dt = ds.Tables[0];
                GridCategoria.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    LblDatosNoEncontrados.Visible = false;
                    GridCategoria_CellClick(null, null);

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

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = ValidarDatos();
                if (sResultado == "")
                {

                    if (lbllid.Text == "0")
                    {
                        Categoria categoria = new Categoria();
                        categoria.Descripcion = txtNombre.Text;
                        

                        if (FCategoria.Insertar(categoria) > 0)
                        {
                            MessageBox.Show("Datos Guardados Correctamente");
                            FrmCategoria_Load(null, null);
                        }

                    }
                    else
                    {
                        Categoria categoria = new Categoria();
                        categoria.Descripcion = txtNombre.Text;
                        categoria.Id = Convert.ToInt32(lbllid.Text);
                       

                        if (FCategoria.Actualizar(categoria) == 1)
                        {
                            MessageBox.Show("Datos Modificados Correctamente");
                            FrmCategoria_Load(null, null);
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

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(false);
            GridCategoria_CellClick(null, null);
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
            txtNombre.Text = "";
            lbllid.Text = "0";

        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);

        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                if (MessageBox.Show("¿Desea Eliminar las Categorias Seleccionados?", "Eliminacion de Categoria", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in GridCategoria.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Eliminar"].Value))
                        {
                            Categoria categoria = new Categoria();
                            categoria.Id = Convert.ToInt32(row.Cells["Id"].Value);
                            if (FCategoria.Eliminar(categoria) != 1)
                            {
                                MessageBox.Show("la Categoria no pudo ser eliminada", "Eliminacion de Categoria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                        }

                    }
                    FrmCategoria_Load(null, null);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(dt.Copy());
                dv.RowFilter = CmbBuscar.Text + " LIKE '" + txtBuscar.Text + "%'";

                GridCategoria.DataSource = dv;
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

        private void GridCategoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == GridCategoria.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)GridCategoria.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);

            }

        }

        private void GridCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (GridCategoria.CurrentRow != null)
            {
                lbllid.Text = GridCategoria.CurrentRow.Cells[1].Value.ToString();
                txtNombre.Text = GridCategoria.CurrentRow.Cells[2].Value.ToString();
               

            }

        }
        public string ValidarDatos()
        {
            string Resultado = "";
            if (txtNombre.Text == "")
            {
                Resultado = Resultado + "Nombre \n";
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
            GridCategoria.Enabled = !b;

            txtNombre.Enabled = b;
            
        }

        private void GridCategoria_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtFlag.Text == "1")
            {

            
            FrmProducto frmProd = FrmProducto.GetInscance();

            if (GridCategoria.CurrentRow != null)
            {
                frmProd.SetCategoria(GridCategoria.CurrentRow.Cells[1].Value.ToString(), GridCategoria.CurrentRow.Cells[2].Value.ToString());
                frmProd.Show();
                Close();


            }
            }
        }
    }
}
