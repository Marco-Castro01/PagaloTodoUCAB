using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class DeleteServicioPruebaCommand : IRequest<Guid>
    {
        public DeleteServicioRequest _request { get; set; }

        public DeleteServicioPruebaCommand(DeleteServicioRequest request)
        {
            _request = request;
        }
    }
}
