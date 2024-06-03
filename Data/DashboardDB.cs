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
    internal class DashboardDB
    {
        string connectionString = "server=localhost;database=cafe_el_angel;user=root;password=Telecastercustom2005";
        MySqlConnection connection;

        //constructor
        public DashboardDB()
        {
            connection = new MySqlConnection(connectionString);
        }


        public Usuario ValidarUsuario(string email, string contraseña)
        {
            try
            {
                string query = "SELECT * FROM usuarios WHERE email = @Email AND contraseña = @Contraseña";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Contraseña", contraseña);

                connection.Open();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario
                        {
                            Id = reader.GetInt32("id"),
                            NombreUsuario = reader.GetString("nombre_usuario"),
                            Email = reader.GetString("email"),
                            Contraseña = reader.GetString("contraseña"),
                            FechaCreacion = reader.GetDateTime("fecha_creacion"),
                            Rol = reader.GetString("rol")
                        };
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar el usuario: " + ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }




        public DataTable LeerTablaReservasHoy()
        {
            DataTable ReservasDelDia = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Consulta para obtener las reservaciones del día actual
                string sql = "SELECT * FROM reservaciones WHERE DATE(fecha_reservacion) = CURDATE()";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(ReservasDelDia);
                    }
                }
            }

            return ReservasDelDia;
        }



    }
}
