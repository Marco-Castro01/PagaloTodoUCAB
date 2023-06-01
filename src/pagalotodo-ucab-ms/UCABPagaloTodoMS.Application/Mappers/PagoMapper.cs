using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

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

        public static PagoEntity MapRequestEntity(PagoRequest request, IUCABPagaloTodoDbContext DbContext)
        {
            var entity = new PagoEntity()
            {
                valor = request.Valor,
                consumidor = DbContext.Consumidor.Find(request.ConsumidorId),
                servicio = DbContext.Servicio.Find(request.ServicioId)
               
            };
            return entity;
        }
        
    }
}