using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Mappers
{
    public static class PrestadorServicioMapper
    {
        public static PrestadorServicioResponse MapEntityAResponse(PrestadorServicioEntity entity)
        {
            var response = new PrestadorServicioResponse()
            {
                Id = entity.Id,
                rif = entity.rif,
                nickName = entity.nickName,
                status = entity.status,
                email = entity.email,
                password = entity.password
            };
            return response;    
        }

        public static PrestadorServicioEntity MapRequestEntity(PrestadorServicioRequest request)
        {
            var entity = new PrestadorServicioEntity()
            {
                rif = request.rif,
                nickName = request.nickName,
                status = request.status,
                email = request.email,
                password = request.password
            };
            return entity;
        }
    }
}