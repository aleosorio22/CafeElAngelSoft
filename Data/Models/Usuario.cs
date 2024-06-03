using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeElAngel.Data.Models
{
    internal class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Rol { get; set; }
    }
}
