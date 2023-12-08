using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.DTOS.CREATE
{
    public class CrearUsuariosDTO
    {
        public string Nombre { get; set; } = null!;

        public string Correoelectronico { get; set; } = null!;

        public string Contraseña { get; set; } = null!;

        public string? Rol { get; set; }

    }
}
