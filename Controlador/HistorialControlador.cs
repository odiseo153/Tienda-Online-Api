using AutoMapper;
using Controlador.Interfazes;
using Microsoft.AspNetCore.Mvc;
using Modelo.DTOS.CREATE_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda_Online_Api.Modelos;

namespace Controlador
{
    public class HistorialControlador 
    {
        private TiendaOnlineContext context;
        private IMapper mapper;

        public HistorialControlador(TiendaOnlineContext tiendaOnlineContext, IMapper mapper)
        {
            this.mapper = mapper;
            context = tiendaOnlineContext;
        }

        public JsonResult ObtenerPorId(string id)
        {
            return new JsonResult(mapper.Map<Historialcambio, CrearHistorialDTO>(context.Historialcambios.FirstOrDefault(x => x.Id.Equals(Guid.Parse(id)))));
        }

        public JsonResult ObtenerTodos()
        {
            return new JsonResult(mapper.Map<List<Historialcambio>,List<CrearHistorialDTO>>(context.Historialcambios.ToList()));
        }
    }
}
