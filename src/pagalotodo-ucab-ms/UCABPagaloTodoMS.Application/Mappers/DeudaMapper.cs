using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Mappers
{
    [ExcludeFromCodeCoverage]
    public static class DeudaMapper
    {
        public static DeudaResponse MapEntityAResponse(DeudaEntity entity)
        {
            var response = new DeudaResponse()
            {
                idDeuda = entity.Id
                
            };
            return response;    
        }

        public static DeudaEntity MapRequestEntity(UpdateDeudaStatusRequest Request, IUCABPagaloTodoDbContext DbContext)
        {

          
            var entity=DbContext.Deuda.FirstOrDefault(u => u.Id == Request.idDeuda);

            entity.deudaStatus = true;
            return entity;
        }
      
        
    }
}