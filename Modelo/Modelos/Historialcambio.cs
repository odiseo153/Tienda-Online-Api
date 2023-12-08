using System;
using System.Collections.Generic;

namespace Tienda_Online_Api.Modelos;

public class Historialcambio
{
    public Guid Id { get; set; }

    public Guid Pedidoid { get; set; }

    public Guid Usuarioid { get; set; }

    public string? Motivo { get; set; }

    public DateTime? Fechadecambio { get; set; }

    public virtual Pedido? Pedido { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
