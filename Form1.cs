using CafeElAngel.Data.Models;
using CafeElAngel.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeElAngel
{
    public partial class Form1 : Form
    {

        DashboardDB conec = new DashboardDB();

        public Form1()
        {
            InitializeComponent();
        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "USUARIO")
            {
                txtUser.Text = string.Empty;
                txtUser.ForeColor = Color.LightGray;

            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if(txtUser.Text == string.Empty)
            {
                txtUser.Text = "USUARIO";
                txtUser.ForeColor = Color.DimGray;
            }
        }

        private void txtPwd_Enter(object sender, EventArgs e)
        {
            if (txtPwd.Text == "CONTRASEÑA")
            {
                txtPwd.Text = string.Empty;
                txtPwd.ForeColor = Color.LightGray;
                txtPwd.UseSystemPasswordChar = true;

            }
        }

        private void txtPwd_Leave(object sender, EventArgs e)
        {
            if (txtPwd.Text == string.Empty)
            {
                txtPwd.Text = "CONTRASEÑA";
                txtPwd.ForeColor = Color.DimGray;
                txtPwd.UseSystemPasswordChar = false;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = txtUser.Text;
            string contraseña = txtPwd.Text;

            ConexionDB conexion = new ConexionDB();
            Usuario usuario = conec.ValidarUsuario(email, contraseña);

            if (usuario != null)
            {
                MessageBox.Show("Ingreso exitoso. Bienvenido " + usuario.NombreUsuario);
                
                Home main = new Home();
                main.Show();
                this.Hide();

                main.FormClosed += (s, args) => this.Close();
            }
            else
            {
                MessageBox.Show("Correo o contraseña incorrectos.");
            }
        } 
          
    }

}
