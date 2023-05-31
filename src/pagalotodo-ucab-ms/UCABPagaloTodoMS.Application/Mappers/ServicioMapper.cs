using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
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
                prestadorServicioId= entity.PrestadorServicio.Id
                
            };
            return response;    
        }

        public static ServicioEntity MapRequestEntity(ServicioRequest request, IUCABPagaloTodoDbContext DbContext)
        {
            var entity = new ServicioEntity()
            {
               name = request.name,
               accountNumber = request.accountNumber,
               PrestadorServicio = DbContext.PrestadorServicio.Find(request.PrestadorServicioId),
               tipoServicio = request.tipoServicio,
               statusServicio = request.statusServicio
            };
            return entity;
        }
    }
}