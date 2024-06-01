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
    public partial class Reservas : Form
    {

        Reservaciones reservacion = new Reservaciones();
        ConexionDB conec = new ConexionDB();

        public Reservas()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Reservas_Load(object sender, EventArgs e)
        {

            DataTable dt = conec.LeerTabla();
            dataGridViewReservaciones.DataSource = dt;
        }

        private void txtBuscarPorNombre_Enter(object sender, EventArgs e)
        {
            if (txtBuscarPorNombre.Text == "Buscar por nombre")
            {
                txtBuscarPorNombre.Text = string.Empty;
                txtBuscarPorNombre.ForeColor = Color.Black;
            }
        }

        private void txtBuscarPorNombre_Leave(object sender, EventArgs e)
        {
            if (txtBuscarPorNombre.Text == string.Empty)
            {
                txtBuscarPorNombre.Text = "Buscar por nombre";
                txtBuscarPorNombre.ForeColor = Color.DimGray;
            }
        }

        //buscar por nombre cliente
        private void BuscarPorNombreCliente()
        {
            try
            {
                string nombreCliente = txtBuscarPorNombre.Text;

                if (string.IsNullOrWhiteSpace(nombreCliente))
                {
                    MessageBox.Show("Por favor, introduce el nombre del cliente para la búsqueda.");
                    return;
                }

                DataTable reservacionesFiltradas = conec.BuscarReservacionPorNombreCliente(nombreCliente);

                if (reservacionesFiltradas.Rows.Count > 0)
                {
                    dataGridViewReservaciones.DataSource = reservacionesFiltradas;
                }
                else
                {
                    MessageBox.Show("No se encontraron reservaciones con ese nombre de cliente.");
                    DataTable dt = conec.LeerTabla();
                    dataGridViewReservaciones.DataSource = dt;
                    txtBuscarPorNombre.Text = string.Empty;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al buscar las reservaciones: {ex.Message}");
            }
        }

        private void btnBuscarPorNombre_Click(object sender, EventArgs e)
        {
            BuscarPorNombreCliente();
        }

        //buscar reservacion por fecha
        private void BuscarResPorFecha()
        {
            try
            {
                DateTime fechaReservacion = dateTimePickerFechaRes.Value;

                DataTable reservacionesFiltradas = conec.BuscarReservacionPorFecha(fechaReservacion);

                if (reservacionesFiltradas.Rows.Count > 0)
                {
                    dataGridViewReservaciones.DataSource = reservacionesFiltradas;
                }
                else
                {
                    MessageBox.Show("No se encontraron reservaciones para esa fecha.");
                    DataTable dt = conec.LeerTabla();
                    dataGridViewReservaciones.DataSource = dt;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al buscar las reservaciones: {ex.Message}");
            }
        }

        private void btnBuscarPorFecha_Click(object sender, EventArgs e)
        {
            BuscarResPorFecha();
        }

    }
}