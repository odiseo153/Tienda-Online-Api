using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.DTOS.CREATE_DTO
{
    public class CrearHistorialDTO
    {
        public Guid Pedidoid { get; set; }

        public Guid Usuarioid { get; set; }

        public string? Motivo { get; set; }

        public DateTime? Fechadecambio { get; set; }

    }
}
