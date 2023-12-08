using System;
using System.Collections.Generic;

namespace Tienda_Online_Api.Modelos;

public partial class Usuario
{
    public Guid Usuarioid { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correoelectronico { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string? Rol { get; set; }

    public virtual ICollection<Carritocompra> Carritocompras { get; set; } = new List<Carritocompra>();

    public virtual ICollection<Historialcambio> Historialcambios { get; set; } = new List<Historialcambio>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
