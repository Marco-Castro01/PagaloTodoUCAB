using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
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

        public static PagoEntity MapRequestEntity(PagoRequest request)
        {
            var entity = new PagoEntity()
            {
                valor = request.Valor,
            
               
            };
            return entity;
        }
    }
}