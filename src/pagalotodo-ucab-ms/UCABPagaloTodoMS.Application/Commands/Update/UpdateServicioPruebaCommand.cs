using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class UpdateServicioPruebaCommand : IRequest<Guid>
    {
        public UpdateServicioRequest _request { get; set; }

        public UpdateServicioPruebaCommand(UpdateServicioRequest request)
        {
            _request = request;
        }
    }
}
