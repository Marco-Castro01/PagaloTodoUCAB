using MediatR;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class ConsultarAdminInformacionQuery : IRequest<AdminResponse>
    {
        public Guid _idAdmin { get; set; }

        public ConsultarAdminInformacionQuery(Guid idAdmin)
        {
            _idAdmin = idAdmin;
        }
        
    }
}

