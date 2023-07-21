using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetInfoAdminQuery : IRequest<AdminResponse>
    {
        public Guid _idAdmin { get; set; }

        public GetInfoAdminQuery(Guid idAdmin)
        {
            _idAdmin = idAdmin;
        }
        
    }
}

