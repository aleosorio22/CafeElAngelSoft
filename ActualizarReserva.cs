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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


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
                // Captura del valor de Anticipo y Total
                if (!decimal.TryParse(txtAnticipo.Text, out decimal anticipo))
                {
                    MessageBox.Show("Por favor, introduce un valor válido para el anticipo.");
                    return;
                }
                if (!decimal.TryParse(txtTotal.Text, out decimal total))
                {
                    MessageBox.Show("Por favor, introduce un valor válido para el total a pagar.");
                    return;
                }
                string pastel = txtPastel.Text;
                string reposteria = txtReposteria.Text;
                string comida = txtComida.Text;


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
                        TomadaPor = tomadaPor,
                        Anticipo = anticipo,
                        Total = total,
                        Pastel = pastel,
                        Reposteria = reposteria,
                        Comida = comida

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
                    double anticipo = double.Parse(reservacionEncontrada.Rows[0]["anticipo"].ToString()); // Obtener el anticipo de la reserva
                    double total = double.Parse(reservacionEncontrada.Rows[0]["total"].ToString()); // Obtener el total
                    string pastel = reservacionEncontrada.Rows[0]["pastel"].ToString();
                    string reposteria = reservacionEncontrada.Rows[0]["reposteria"].ToString();
                    string comida = reservacionEncontrada.Rows[0]["comida"].ToString();
                    


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
                    txtAnticipo.Value = Convert.ToDecimal(anticipo);
                    txtTotal.Value = Convert.ToDecimal(total);
                    txtPastel.Text = pastel;
                    txtReposteria.Text = reposteria;
                    txtComida.Text = comida;
                }
                else
                {
                    MessageBox.Show("Reservación no encontrada.");
                    txtBuscarIdRes.Text = string.Empty;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, introduzca un ID válido.");
                txtBuscarIdRes.Text = string.Empty;
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
                        txtAnticipo.Value = 0;
                        txtTotal.Value = 0;
                        txtPastel.Text = "";
                        txtReposteria.Text = "";
                        txtComida.Text = "";
                        
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

        private void btnImprimir_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF file|*.pdf";
            saveFileDialog.Title = "Guardar reservación como PDF";
            saveFileDialog.FileName = "Reservacion.pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                CreatePDF(path);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            
        }

        private void CreatePDF(string path)
        {
            Document doc = new Document(PageSize.A4, 30, 30, 30, 30);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                // Centrar el logo
                string logoPath = @"LOGO.png";
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                logo.ScaleToFit(200f, 100f);
                logo.Alignment = Element.ALIGN_CENTER;
                doc.Add(logo);

                // Fuentes
                BaseFont bfTicket = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fontTicket = new iTextSharp.text.Font(bfTicket, 10, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font fontTicketBold = new iTextSharp.text.Font(bfTicket, 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontTicketSmall = new iTextSharp.text.Font(bfTicket, 8, iTextSharp.text.Font.NORMAL);

                Paragraph header = new Paragraph("Café El Angel\nDirección: Aldea El Porvenir, El Progreso, Jutiapa\nTeléfono: 3166-7151", fontTicketSmall);
                header.Alignment = Element.ALIGN_CENTER;
                doc.Add(header);

                // Título
                Paragraph title = new Paragraph("informacion de reservación", fontTicketBold);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingBefore = 15f;
                title.SpacingAfter = 15f;
                doc.Add(title);

                // Línea separadora
                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_CENTER, 1)));
                doc.Add(line);

                // Información de la reservación
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1f, 2f });
                table.SpacingBefore = 10f;
                table.SpacingAfter = 10f;

                AddRowToTable(table, "Nombre:", txtNombreCliente.Text, fontTicket);
                AddRowToTable(table, "Email:", txtEmailCliente.Text, fontTicket);
                AddRowToTable(table, "Teléfono:", txtTelefonoCliente.Text, fontTicket);
                AddRowToTable(table, "Fecha:", dateTimePickerFechaReservacion.Text, fontTicket);
                AddRowToTable(table, "Hora:", maskedTextBoxHoraReservacion.Text, fontTicket);
                AddRowToTable(table, "Personas:", txtNumeroPersonas.Text, fontTicket);
                AddRowToTable(table, "Tipo:", comboBoxTipo.Text, fontTicket);
                AddRowToTable(table, "Notas:", txtNotas.Text, fontTicket);
                AddRowToTable(table, "Tomada por:", txtTomadaPor.Text, fontTicket);
                AddRowToTable(table, "Estado:", comboBoxEstado.Text, fontTicket);
                AddRowToTable(table, "Anticipo:", txtAnticipo.Text, fontTicket);
                AddRowToTable(table, "Total a pagar", txtTotal.Text, fontTicket);
                AddRowToTable(table, "Pastel:", txtPastel.Text, fontTicket);
                AddRowToTable(table, "Repostería:", txtReposteria.Text, fontTicket);
                AddRowToTable(table, "Comida/Bebida:", txtComida.Text, fontTicket);

                doc.Add(table);

                // Otra línea separadora
                doc.Add(line);

                // Pie de página
                Paragraph footer = new Paragraph("¡Gracias por su reservación!", fontTicketBold);
                footer.Alignment = Element.ALIGN_CENTER;
                footer.SpacingBefore = 10f;
                doc.Add(footer);

                Paragraph terms = new Paragraph("Términos y condiciones: Las reservaciones tienen un limite de tiempo de 3 horas a partir de la hora en que se regitro la reservacion.", fontTicketSmall);
                terms.Alignment = Element.ALIGN_CENTER;
                terms.SpacingBefore = 10f;
                doc.Add(terms);

                // Añadir número de reservación y código QR
                string reservationNumber = GenerateReservationNumber();
                Paragraph reservationParagraph = new Paragraph("Número de reservación: " + reservationNumber, fontTicketBold);
                reservationParagraph.Alignment = Element.ALIGN_CENTER;
                reservationParagraph.SpacingBefore = 20f;
                doc.Add(reservationParagraph);

                BarcodeQRCode qrCode = new BarcodeQRCode(reservationNumber, 100, 100, null);
                iTextSharp.text.Image qrCodeImage = qrCode.GetImage();
                qrCodeImage.Alignment = Element.ALIGN_CENTER;
                qrCodeImage.ScaleAbsolute(80, 80);
                doc.Add(qrCodeImage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                doc.Close();
            }
            MessageBox.Show("PDF generado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AddRowToTable(PdfPTable table, string label, string value, iTextSharp.text.Font font)
        {
            PdfPCell cellLabel = new PdfPCell(new Phrase(label, font));
            PdfPCell cellValue = new PdfPCell(new Phrase(value, font));
            cellLabel.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellValue.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellLabel.PaddingBottom = 5f;
            cellValue.PaddingBottom = 5f;
            table.AddCell(cellLabel);
            table.AddCell(cellValue);
        }

        private string GenerateReservationNumber()
        {
            return "RES" + DateTime.Now.ToString("yyyyMMddHHmmss");
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

        private void txtBuscarIdRes_Enter(object sender, EventArgs e)
        {
            if (txtBuscarIdRes.Text == "Buscar por ID")
            {
                txtBuscarIdRes.Text =string.Empty;
                txtBuscarIdRes.ForeColor = Color.Black;
            }
        }

        private void txtBuscarIdRes_Leave(object sender, EventArgs e)
        {
            if (txtBuscarIdRes.Text == string.Empty)
            {
                txtBuscarIdRes.Text = "Buscar por ID";
                txtBuscarIdRes.ForeColor = Color.DimGray;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtAnticipo_ValueChanged(object sender, EventArgs e)
        {

        }

        
    }
}
