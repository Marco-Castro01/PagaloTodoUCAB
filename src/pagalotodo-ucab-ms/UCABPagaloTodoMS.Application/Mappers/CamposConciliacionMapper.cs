using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Mappers
{
    public static class CamposConciliacionMapper
    {
        public static CamposConciliacionResponse MapEntityAResponse(CamposConciliacionEntity entity)
        {
            var response = new CamposConciliacionResponse()
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Longitud = entity.Longitud
            };
            return response;    
        }

        public static CamposConciliacionEntity MapRequestEntity(CamposConciliacionRequest request)
        {
            var entity = new CamposConciliacionEntity()
            {
               Nombre = request.Nombre,
               Longitud = request.Longitud
            };
            return entity;
        }
    }
}