using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Mappers
{
    public static class ServicioMapper
    {
        public static ServicioResponse MapEntityAResponse(ServicioEntity entity)
        {
            var response = new ServicioResponse()
            {
                Id = entity.Id,
                name = entity.name,
                accountNumber = entity.accountNumber,
                PrestadorServicioId = entity.PrestadorServicio
                
            };
            return response;    
        }

        public static ServicioEntity MapRequestEntity(ServicioRequest request)
        {
            var entity = new ServicioEntity()
            {
               name = request.name,
               accountNumber = request.accountNumber,
            };
            return entity;
        }
    }
}