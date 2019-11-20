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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            DataSet ds = FLogin.ValidarLogin(txtUsuario.Text, txtPassword.Text);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count>0)
            {
                Usuario.Apellido = dt.Rows[0]["Apellido"].ToString();
                Usuario.Nombre = dt.Rows[0]["Nombre"].ToString();
                Usuario.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                Usuario.Dni = Convert.ToInt32(dt.Rows[0]["Dni"]);
                Usuario.NombreUsuario = dt.Rows[0]["Usuario"].ToString();
                Usuario.Tipo = dt.Rows[0]["Tipo"].ToString();
                Usuario.Telefono = dt.Rows[0]["Telefono"].ToString();
                Usuario.Direccion = dt.Rows[0]["Direccion"].ToString();

                FrmVenta.GetInscance().Show();
            }
            else
            {
                MessageBox.Show("Usuario y/o Password Incorrectas");
                txtPassword.Text = "";
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
