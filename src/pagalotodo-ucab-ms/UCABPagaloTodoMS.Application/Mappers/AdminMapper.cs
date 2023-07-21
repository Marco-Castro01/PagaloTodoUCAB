using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Mappers
{
    [ExcludeFromCodeCoverage]
    public static class AdminMapper
    {
        public static AdminResponse MapEntityAResponse(AdminEntity entity)
        {
            var response = new AdminResponse()
            {
                Id = entity.Id,
                cedula = entity.cedula,
                nickName = entity.nickName,
                status = entity.status,
                email = entity.email,
            };
            return response;    
        }

        public static AdminEntity MapRequestEntity(AdminRequest request)
        {
            var entity = new AdminEntity()
            {
                cedula = request.cedula,
                nickName = request.nickName,
                email = request.email,
            };
            return entity;
        }
    }
}