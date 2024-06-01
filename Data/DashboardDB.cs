using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
