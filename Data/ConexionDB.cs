using CafeElAngel.Data.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeElAngel.Data
{
    internal class ConexionDB
    {
        string connectionString = "server=localhost;database=cafe_el_angel;user=root;password=Telecastercustom2005";
        MySqlConnection connection;

        //constructor
        public ConexionDB()
        {
            connection = new MySqlConnection(connectionString);
        }

        //insertar
            public bool Insertar(Reservaciones reservacion)
            {
                try
                {
                    string query = "INSERT INTO reservaciones (nombre_cliente, email_cliente, telefono_cliente, fecha_reservacion, hora_reservacion, numero_personas, tipo, notas, estado, fecha_creacion, tomada_por) VALUES (@nombreCliente, @emailCliente, @telefonoCliente, @fechaReservacion, @horaReservacion, @numeroPersonas, @tipo, @notas, @estado, @fechaCreacion, @tomadaPor)";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@nombreCliente", reservacion.NombreCliente);
                    command.Parameters.AddWithValue("@emailCliente", reservacion.EmailCliente);
                    command.Parameters.AddWithValue("@telefonoCliente", reservacion.TelefonoCliente);
                    command.Parameters.AddWithValue("@fechaReservacion", reservacion.FechaReservacion);
                    command.Parameters.AddWithValue("@horaReservacion", reservacion.HoraReservacion);
                    command.Parameters.AddWithValue("@numeroPersonas", reservacion.NumeroPersonas);
                    command.Parameters.AddWithValue("@tipo", reservacion.Tipo);
                    command.Parameters.AddWithValue("@notas", reservacion.Notas);
                    command.Parameters.AddWithValue("@estado", reservacion.Estado);
                    command.Parameters.AddWithValue("@fechaCreacion", reservacion.FechaCreacion);
                    command.Parameters.AddWithValue("@tomadaPor", reservacion.TomadaPor);

                    connection.Open();
                    command.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el registro: " + ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }


        public DataTable LeerTabla()
        {
            DataTable tablaReservas = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM reservaciones";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(tablaReservas);
                    }
                }
            }

            return tablaReservas;
        }

        //buscar reservacion por el nombre del cliente
        public DataTable BuscarReservacionPorNombreCliente(string nombreCliente)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM reservaciones WHERE nombre_cliente LIKE @nombreCliente";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nombreCliente", "%" + nombreCliente + "%");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        //bucsar reservacion por fecha
        public DataTable BuscarReservacionPorFecha(DateTime fechaReservacion)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM reservaciones WHERE DATE(fecha_reservacion) = @fechaReservacion";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@fechaReservacion", fechaReservacion.ToString("yyyy-MM-dd"));
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        //buscar reserva por id
        public DataTable BuscarReservacionPorId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM reservaciones WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }



        //actualizar datos reservacion
        public bool ActualizarReservacion(Reservaciones reservacion)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE reservaciones SET nombre_cliente = @nombreCliente, email_cliente = @emailCliente, telefono_cliente = @telefonoCliente, fecha_reservacion = @fechaReservacion, hora_reservacion = @horaReservacion, numero_personas = @numeroPersonas, tipo = @tipo, notas = @notas, estado = @estado, tomada_por = @tomadaPor, fecha_creacion = CURRENT_TIMESTAMP WHERE id = @id";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", reservacion.Id);
                        command.Parameters.AddWithValue("@nombreCliente", reservacion.NombreCliente);
                        command.Parameters.AddWithValue("@emailCliente", reservacion.EmailCliente);
                        command.Parameters.AddWithValue("@telefonoCliente", reservacion.TelefonoCliente);
                        command.Parameters.AddWithValue("@fechaReservacion", reservacion.FechaReservacion);
                        command.Parameters.AddWithValue("@horaReservacion", reservacion.HoraReservacion);
                        command.Parameters.AddWithValue("@numeroPersonas", reservacion.NumeroPersonas);
                        command.Parameters.AddWithValue("@tipo", reservacion.Tipo);
                        command.Parameters.AddWithValue("@notas", reservacion.Notas);
                        command.Parameters.AddWithValue("@estado", reservacion.Estado);
                        command.Parameters.AddWithValue("@tomadaPor", reservacion.TomadaPor);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la reservación: " + ex.Message);
                return false;
            }
        }

        //eliminar reservacion por ID
        public bool EliminarReservacionPorId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM reservaciones WHERE id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }





    }
}
