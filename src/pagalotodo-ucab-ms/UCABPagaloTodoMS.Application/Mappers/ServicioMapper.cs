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
        public static ServicioEntity MapRequestUpdateEntity(UpdateServicioRequest request, IUCABPagaloTodoDbContext DbContext)
        {
            
            var entity=DbContext.Servicio.Where(u => u.Id == request.idServicio && u.deleted==false).FirstOrDefault();
            entity.name = request.name;
            entity.accountNumber = request.accountNumber;
            entity.tipoServicio = request.tipoServicio;
            entity.statusServicio = request.statusServicio;
            entity.UpdatedAt=DateTime.Now;
            entity.UpdatedBy = "APP";
            
            return entity;
        }
        public static ServicioEntity MapRequestDeleteEntity(DeleteServicioRequest request, IUCABPagaloTodoDbContext DbContext)
        {
            
            var entity=DbContext.Servicio.Where(u => u.Id == request.idServicio && u.deleted==false).FirstOrDefault();
            entity.deleted = request.delete;
            
            return entity;
        }
    }
}