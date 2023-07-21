using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Mappers
{
    [ExcludeFromCodeCoverage]
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

        public static ServicioEntity MapRequestEntity(Guid prestadorServicioId,ServicioRequest request, IUCABPagaloTodoDbContext DbContext)
        {
            var entity = new ServicioEntity()
            {
               name = request.name,
               accountNumber = request.accountNumber,
               PrestadorServicio = DbContext.PrestadorServicio.FirstOrDefault(x=>x.Id==prestadorServicioId && x.deleted==false),
               tipoServicio = request.tipoServicio,
               statusServicio = request.statusServicio,
               
               
            };
            return entity;
        }
        public static ServicioEntity MapRequestUpdateEntity(UpdateServicioPruebaCommand request, IUCABPagaloTodoDbContext DbContext)
        {
            
            var entity=DbContext.Servicio.FirstOrDefault(u => u.Id == request._idServicio && u.deleted==false);
            entity.name = request._request.name;
            entity.accountNumber = request._request.accountNumber;
            entity.tipoServicio = request._request.tipoServicio;
            entity.statusServicio = request._request.statusServicio;
            entity.UpdatedAt=DateTime.Now;
            entity.UpdatedBy = "APP";
            
            return entity;
        }
        public static ServicioEntity MapRequestDeleteEntity(DeleteServicioPruebaCommand request, IUCABPagaloTodoDbContext DbContext)
        {
            
            var entity=DbContext.Servicio.FirstOrDefault(u => u.Id == request._idServicio && u.deleted==false);
            return entity;
        }
    }
}