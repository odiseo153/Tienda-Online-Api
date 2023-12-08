using System;

namespace Tienda_Online_Api.Modelos;

public class Detallespedido
{
    public Guid Detallepedidoid { get; set; }

    public Guid Pedidoid { get; set; }

    public Guid Productoid { get; set; }

    public int Cantidad { get; set; }

    public decimal Preciounitario { get; set; }

    public virtual Pedido? Pedido { get; set; }

    public virtual Producto? Producto { get; set; }
}
