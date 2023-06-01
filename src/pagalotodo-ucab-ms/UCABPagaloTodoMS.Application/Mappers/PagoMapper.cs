using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Mappers
{
    public static class PagoMapper
    {
        public static PagoResponse MapEntityAResponse(PagoEntity entity)
        {
            var response = new PagoResponse()
            {
                Id = entity.Id,
                valor = entity.valor,
                
            };
            return response;    
        }

        public static PagoEntity MapRequestDirectoEntity(PagoDirectoRequest directoRequest, IUCABPagaloTodoDbContext DbContext)
        {
            
            var entity = new PagoEntity()
            {
                valor = directoRequest.Valor,
                consumidor = DbContext.Consumidor.Find(directoRequest.ConsumidorId),
                servicio = DbContext.Servicio.Find(directoRequest.ServicioId),
                

            };
            return entity;
        }
        public static PagoEntity MapRequestPorValidacionEntity(PagoPorValidacionRequest Request, IUCABPagaloTodoDbContext DbContext)
        {
            
            var entity = new PagoEntity()
            {
                valor = DbContext.Deuda.Find(Request.IdDeuda)?.deuda,
                consumidor = DbContext.Consumidor.Find(Request.ConsumidorId),
                servicio = DbContext.Servicio.Find(Request.ServicioId),
                

            };
            return entity;
        }
        
    }
}