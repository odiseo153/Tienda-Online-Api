using System;
using System.Collections.Generic;

namespace Tienda_Online_Api.Modelos;

public partial class Producto
{
    public Guid Productoid { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<Carritocompra> Carritocompras { get; set; } = new List<Carritocompra>();

    public virtual ICollection<Detallespedido> Detallespedidos { get; set; } = new List<Detallespedido>();
}
