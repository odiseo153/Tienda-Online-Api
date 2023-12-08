using System;
using System.Collections.Generic;

namespace Tienda_Online_Api.Modelos;

public class Carritocompra
{
    public Guid Carritoid { get; set; }

    public Guid Usuarioid { get; set; }

    public Guid Productoid { get; set; }

    public int Cantidad { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
