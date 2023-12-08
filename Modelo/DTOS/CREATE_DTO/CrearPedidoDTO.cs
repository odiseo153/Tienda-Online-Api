using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.DTOS.CREATE_DTO
{
    public class CrearPedidoDTO
    {
        public Guid Usuarioid { get; set; }

        public Guid Productoid { get; set; }

        public int Cantidad { get; set; }

    }
}
