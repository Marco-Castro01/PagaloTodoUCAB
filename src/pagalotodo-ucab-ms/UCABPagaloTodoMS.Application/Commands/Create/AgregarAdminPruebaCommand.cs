using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AgregarAdminPruebaCommand : IRequest<Guid>
    {
        public AdminRequest _request { get; set; }

        public AgregarAdminPruebaCommand(AdminRequest request)
        {
            _request = request;
        }
    }
}
