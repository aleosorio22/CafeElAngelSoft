using CafeElAngel.Data;
using CafeElAngel.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CafeElAngel
{
    public partial class ActualizarReserva : Form
    {
        Reservaciones reservacion = new Reservaciones();
        ConexionDB conec = new ConexionDB();

        public ActualizarReserva()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //actualizar datos
        private void ActualizarReservacion()
        {
            try
            {
                int idReservacion = int.Parse(txtBuscarIdRes.Text);
                string nombreCliente = txtNombreCliente.Text;
                string emailCliente = txtEmailCliente.Text;
                string telefonoCliente = txtTelefonoCliente.Text;
                DateTime fechaReservacion = dateTimePickerFechaReservacion.Value;
                TimeSpan horaReservacion = TimeSpan.Parse(maskedTextBoxHoraReservacion.Text);
                int numeroPersonas = (int)txtNumeroPersonas.Value;
                string tipo = comboBoxTipo.Text;
                string notas = txtNotas.Text;
                string estado = comboBoxEstado.Text;
                string tomadaPor = txtTomadaPor.Text;

                // Preguntar si desea actualizar la reservación
                DialogResult result = MessageBox.Show("¿Estás seguro que deseas actualizar los datos de esta reservación?",
                                                      "Confirmación de actualización",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Reservaciones reservacion = new Reservaciones
                    {
                        Id = idReservacion,
                        NombreCliente = nombreCliente,
                        EmailCliente = emailCliente,
                        TelefonoCliente = telefonoCliente,
                        FechaReservacion = fechaReservacion,
                        HoraReservacion = horaReservacion,
                        NumeroPersonas = numeroPersonas,
                        Tipo = tipo,
                        Notas = notas,
                        Estado = estado,
                        TomadaPor = tomadaPor
                    };

                    bool actualizado = conec.ActualizarReservacion(reservacion);

                    if (actualizado)
                    {
                        MessageBox.Show("Reservación actualizada correctamente.");
                        DataTable dt = conec.LeerTabla();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar la reservación. Verifica que el ID sea correcto.");
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, introduce un ID válido y asegúrate de completar los campos correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
        }



        //buscar id
        private void BuscarReservacionPorId()
        {
            try
            {
                int idReservacion = int.Parse(txtBuscarIdRes.Text);
                DataTable reservacionEncontrada = conec.BuscarReservacionPorId(idReservacion);
                if (reservacionEncontrada.Rows.Count > 0)
                {
                    string nombreCliente = reservacionEncontrada.Rows[0]["nombre_cliente"].ToString();
                    string emailCliente = reservacionEncontrada.Rows[0]["email_cliente"].ToString();
                    string telefonoCliente = reservacionEncontrada.Rows[0]["telefono_cliente"].ToString();
                    DateTime fechaReservacion = DateTime.Parse(reservacionEncontrada.Rows[0]["fecha_reservacion"].ToString());
                    TimeSpan horaReservacion = TimeSpan.Parse(reservacionEncontrada.Rows[0]["hora_reservacion"].ToString());
                    int numeroPersonas = int.Parse(reservacionEncontrada.Rows[0]["numero_personas"].ToString());
                    string tipo = reservacionEncontrada.Rows[0]["tipo"].ToString();
                    string notas = reservacionEncontrada.Rows[0]["notas"].ToString();
                    string estado = reservacionEncontrada.Rows[0]["estado"].ToString();
                    string tomadaPor = reservacionEncontrada.Rows[0]["tomada_por"].ToString();

                    // Asignar los valores a los controles de tu formulario
                    txtNombreCliente.Text = nombreCliente;
                    txtEmailCliente.Text = emailCliente;
                    txtTelefonoCliente.Text = telefonoCliente;
                    dateTimePickerFechaReservacion.Value = fechaReservacion;
                    maskedTextBoxHoraReservacion.Text = horaReservacion.ToString(@"hh\:mm");
                    txtNumeroPersonas.Text = numeroPersonas.ToString();
                    comboBoxTipo.Text = tipo;
                    txtNotas.Text = notas;
                    comboBoxEstado.Text = estado;
                    txtTomadaPor.Text = tomadaPor;
                }
                else
                {
                    MessageBox.Show("Reservación no encontrada.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, introduzca un ID válido.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
        }


        //eliminar reservacion por id
        private void BuscarYEliminarReservacionPorId()
        {
            int IdReservacion;

            if (!int.TryParse(txtBuscarIdRes.Text, out IdReservacion))
            {
                MessageBox.Show("Por favor, introduce un ID válido.");
                return;
            }

            DataTable reservacionEncontrada = conec.BuscarReservacionPorId(IdReservacion);

            if (reservacionEncontrada.Rows.Count > 0)
            {
                string nombreCliente = reservacionEncontrada.Rows[0]["nombre_cliente"].ToString();
                string mensajeConfirmacion = $"¿Estás seguro que deseas eliminar la reservación de {nombreCliente}?";
                DialogResult result = MessageBox.Show(mensajeConfirmacion, "Confirmación de eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool eliminada = conec.EliminarReservacionPorId(IdReservacion);
                    if (eliminada)
                    {
                        MessageBox.Show("Reservación eliminada correctamente.");
                        // Limpiar los campos
                        txtNombreCliente.Clear();
                        txtEmailCliente.Clear();
                        txtTelefonoCliente.Clear();
                        maskedTextBoxHoraReservacion.Clear();
                        txtNumeroPersonas.Text = "0";
                        comboBoxTipo.Text = "";
                        txtNotas.Clear();
                        comboBoxEstado.Text = "";
                        txtTomadaPor.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la reservación.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Reservación no encontrada.");
            }
        }



        private void btnBuscarIdRes_Click(object sender, EventArgs e)
        {
            BuscarReservacionPorId();
        }

        private void btnActualizarRes_Click(object sender, EventArgs e)
        {
            ActualizarReservacion();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            BuscarYEliminarReservacionPorId();
        }
    }
}
