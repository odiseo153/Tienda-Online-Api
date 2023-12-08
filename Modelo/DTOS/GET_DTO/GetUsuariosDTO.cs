using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.DTOS.GET
{
    public class GetUsuariosDTO
    {
        public Guid Usuarioid { get; set; }
        public string Nombre { get; set; } = null!;

        public string Correoelectronico { get; set; } = null!;

        public string? Rol { get; set; }

    }
}
