using Newtonsoft.Json;
using UCABPagaloTodoMS.Application.Commands;
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

        public static PagoEntity MapRequestDirectoEntity(AgregarPagoDirectoCommand request, IUCABPagaloTodoDbContext DbContext)
        {
            
            var entity = new PagoEntity()
            {
                Id = new Guid(),
                valor = request.DirectoRequest.Valor,
                consumidor = DbContext.Consumidor.Find(request._idConsumidor),
                servicio = DbContext.Servicio.Find(request._idServicio),
                

            };
            return entity;
        }
        public static PagoEntity MapRequestPorValidacionEntity(DeudaEntity deuda,Guid consumidorId, IUCABPagaloTodoDbContext DbContext)
        {
            
            var entity = new PagoEntity()
            {
                valor = deuda.deuda,
                consumidor = DbContext.Consumidor.Find(consumidorId),
                servicio = deuda.servicio
                

            };
            return entity;
        }
        
    }
}