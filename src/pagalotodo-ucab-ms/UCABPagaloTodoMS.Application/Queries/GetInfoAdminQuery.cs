using MediatR;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class GetInfoAdminQuery : IRequest<AdminResponse>
    {
        public Guid _idAdmin { get; set; }

        public GetInfoAdminQuery(Guid idAdmin)
        {
            _idAdmin = idAdmin;
        }
        
    }
}

