using AutoMapper;
using Modelo.DTOS.CREATE;
using Modelo.DTOS.CREATE_DTO;
using Modelo.DTOS.GET;
using Modelo.DTOS.GET_DTO;
using Tienda_Online_Api.Modelos;

namespace Tienda_Online_Api
{
    public class MappingProfile :Profile
    {
        public MappingProfile() {

            CreateMap<Usuario,GetUsuariosDTO>();
            CreateMap<CrearUsuariosDTO, Usuario>();

            CreateMap<Producto, CrearProductosDTO >();
            CreateMap<CrearProductosDTO,Producto>();

            CreateMap<CrearHistorialDTO, Historialcambio>();
            CreateMap<Historialcambio, CrearHistorialDTO>();

            CreateMap<CrearPedidoDTO, Pedido>();
            CreateMap<Pedido, CrearPedidoDTO>();
        } 
    }
}
