using SistemaVentas.Datos;
using SistemaVentas.Entidades;
using SistemaVentas.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion
{
    public partial class FrmProducto : Form
    {
        private static DataTable dt = new DataTable();
        private static FrmProducto _instancia;
        public FrmProducto()
        {
            InitializeComponent();
        }
        public static FrmProducto GetInscance()
        {
            if (_instancia == null)
                _instancia = new FrmProducto();
            return _instancia;
        }

        public void SetFlag(string sValor)
        {
            txtFlag.Text = sValor;

        }

        public void SetCategoria(string id , string descripcion)
        {
            txtCategoriaId.Text = id;
            txtCategoriaDescripcion.Text = descripcion;
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {

            try
            {
                DataSet ds = FProducto.GetAll();
                dt = ds.Tables[0];
                GridProducto.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    GridProducto.Columns["Imagen"].Visible = false;
                    LblDatosNoEncontrados.Visible = false;
                    GridProducto_CellClick(null, null);

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
            BtnNuevo.Visible = !b;
            BtnEditar.Visible = !b;
            BtnEliminar.Visible = !b;
            GridProducto.Enabled = !b;

            txtNombre.Enabled = b;
            BtnCambiar.Visible = b;
            BtnQuitar.Visible = b;
            BtnBuscarCategoria.Visible = b;

            txtNombre.Enabled = b; 
            txtCategoriaDescripcion.Enabled = b;
            txtCategoriaId.Enabled = b;
            txtDescripcion.Enabled = b;
            txtStock.Enabled = b;
            txtPrecioCompra.Enabled = b;
            txtPrecioVenta.Enabled = b;
            txtFechaVencimiento.Enabled = b;


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
                        Producto producto = new Producto();
                        producto.Categoria.Id = Convert.ToInt32(txtCategoriaId.Text);
                        producto.Nombre = txtNombre.Text;
                        producto.Descripcion = txtDescripcion.Text;
                        producto.Stock= Convert.ToDouble(txtStock.Text);
                        producto.PrecioCompra = Convert.ToDouble(txtPrecioCompra.Text);
                        producto.PrecioVenta = Convert.ToDouble(txtPrecioVenta.Text);
                        producto.FechaVencimiento = txtFechaVencimiento.Value;

                        MemoryStream ms = new MemoryStream();

                        if (Imagen.Image != null)
                        {
                            Imagen.Image.Save(ms, Imagen.Image.RawFormat);
                        }
                        else
                        {
                            Imagen.Image = Resources.transparente;
                            Imagen.Image.Save(ms, Imagen.Image.RawFormat);
                        }
                        producto.Imagen = ms.GetBuffer();


                        if (FProducto.Insertar(producto) > 0)
                        {
                            MessageBox.Show("Datos Guardados Correctamente");
                            FrmProducto_Load(null, null);
                        }

                    }
                    else
                    {
                        Producto producto = new Producto();
                        producto.Id = Convert.ToInt32(lbllid.Text);
                        producto.Categoria.Id = Convert.ToInt32(txtCategoriaId.Text);
                        producto.Nombre = txtNombre.Text;
                        producto.Descripcion = txtDescripcion.Text;
                        producto.Stock = Convert.ToDouble(txtStock.Text);
                        producto.PrecioCompra = Convert.ToDouble(txtPrecioCompra.Text);
                        producto.PrecioVenta = Convert.ToDouble(txtPrecioVenta.Text);
                        producto.FechaVencimiento = txtFechaVencimiento.Value;

                        MemoryStream ms = new MemoryStream();

                        if (Imagen.Image != null)
                        {
                            Imagen.Image.Save(ms, Imagen.Image.RawFormat);
                        }
                        else
                        {
                            Imagen.Image = Resources.transparente;
                            Imagen.Image.Save(ms, Imagen.Image.RawFormat);
                        }
                        producto.Imagen = ms.GetBuffer();

                        if (FProducto.Actualizar(producto) == 1)
                        {
                            MessageBox.Show("Datos Modificados Correctamente");
                            FrmProducto_Load(null, null);
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
        public string ValidarDatos()
        {
            string Resultado = "";
            if (txtNombre.Text == "")
            {
                Resultado = Resultado + "Nombre \n";
            }
           

            return Resultado;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(false);
            GridProducto_CellClick(null, null);
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
            txtNombre.Text = "";
            txtCategoriaDescripcion.Text = "";
            txtCategoriaId.Text = "";
            txtDescripcion.Text = "";
            txtStock.Text = "";
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";
            txtFechaVencimiento.Text = "";
            
            txtBuscar.Text = "";
            lbllid.Text = "0";


            Imagen.BackgroundImage = Resources.transparente;
            Imagen.Image = null;
            Imagen.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea Eliminar los producto Seleccionados?", "Eliminacion de Producto", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in GridProducto.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Eliminar"].Value))
                        {
                            Producto producto = new Producto();
                            producto.Id = Convert.ToInt32(row.Cells["Id"].Value);
                            if (FProducto.Eliminar(producto) != 1)
                            {
                                MessageBox.Show("el producto no pudo ser eliminada", "Eliminacion de producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                        }

                    }
                    FrmProducto_Load(null, null);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }
        }

        private void GridProducto_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (GridProducto.CurrentRow != null)
            {
                lbllid.Text = GridProducto.CurrentRow.Cells["Id"].Value.ToString();
                txtCategoriaId.Text = GridProducto.CurrentRow.Cells["CategoriaId"].Value.ToString();
                txtCategoriaDescripcion.Text = GridProducto.CurrentRow.Cells["CategoriaDescripcion"].Value.ToString();
                txtNombre.Text = GridProducto.CurrentRow.Cells["Nombre"].Value.ToString();
                txtDescripcion.Text = GridProducto.CurrentRow.Cells["Descripcion"].Value.ToString();
                txtStock.Text = GridProducto.CurrentRow.Cells["Stock"].Value.ToString();
                txtPrecioCompra.Text = GridProducto.CurrentRow.Cells["PrecioCompra"].Value.ToString();
                txtPrecioVenta.Text = GridProducto.CurrentRow.Cells["PrecioVenta"].Value.ToString();
                txtFechaVencimiento.Text = GridProducto.CurrentRow.Cells["FechaVencimiento"].Value.ToString();

                Imagen.BackgroundImage = null;
                byte[] b = (byte []) GridProducto.CurrentRow.Cells["Imagen"].Value;
                MemoryStream ms = new MemoryStream(b);
                Imagen.Image = Image.FromStream(ms);
                Imagen.SizeMode = PictureBoxSizeMode.StretchImage;

            }

        }

        private void GridProducto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == GridProducto.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)GridProducto.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);

            }

        }

        private void BtnCambiar_Click(object sender, EventArgs e)
        {
            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                Imagen.BackgroundImage = null;
                Imagen.Image = new Bitmap(dialogo.FileName);
                Imagen.SizeMode = PictureBoxSizeMode.StretchImage;

            }

        }

        private void BtnQuitar_Click(object sender, EventArgs e)
        {
            Imagen.BackgroundImage = Resources.transparente;
            Imagen.Image = null;
            Imagen.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void BtnBuscarCategoria_Click(object sender, EventArgs e)
        {
            FrmCategoria frmcate = new FrmCategoria();
            frmcate.SetFlag("1");
            frmcate.ShowDialog();

        }

        private void GridProducto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (txtFlag.Text == "1")
            {


                FrmDetalleVenta frmDetVentas = FrmDetalleVenta.GetInstance();

                if (GridProducto.CurrentRow != null)
                {
                    Producto producto = new Producto();
                    producto.Id = Convert.ToInt32(GridProducto.CurrentRow.Cells["Id"].Value.ToString());
                    producto.Nombre = GridProducto.CurrentRow.Cells["Nombre"].Value.ToString();
                    producto.Stock = Convert.ToDouble(GridProducto.CurrentRow.Cells["Stock"].Value.ToString());
                    producto.PrecioVenta = Convert.ToDouble(GridProducto.CurrentRow.Cells["PrecioVenta"].Value.ToString());
                    frmDetVentas.SetProducto(producto);
                    frmDetVentas.Show();
                    Close();


                }
            }
        }
    }
}
