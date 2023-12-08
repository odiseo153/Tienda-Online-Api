using System;
using System.Collections.Generic;

namespace Tienda_Online_Api.Modelos;

public partial class Pedido
{
    public Guid Pedidoid { get; set; }

    public Guid Usuarioid { get; set; }

    public DateTime? Fechapedido { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Detallespedido> Detallespedidos { get; set; } = new List<Detallespedido>();

    public virtual ICollection<Historialcambio> Historialcambios { get; set; } = new List<Historialcambio>();

    public virtual Usuario? Usuario { get; set; }
}
