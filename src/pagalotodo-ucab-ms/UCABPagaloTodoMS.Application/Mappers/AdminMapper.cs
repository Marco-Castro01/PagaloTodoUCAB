using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Mappers
{
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
                password = entity.password
            };
            return response;    
        }

        public static AdminEntity MapRequestEntity(AdminRequest request)
        {
            var entity = new AdminEntity()
            {
                cedula = request.cedula,
                nickName = request.nickName,
                status = request.status,
                email = request.email,
                password = request.password
            };
            return entity;
        }
    }
}