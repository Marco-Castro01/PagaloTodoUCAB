using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AgregarServicioPruebaCommand : IRequest<Guid>
    {
        public ServicioRequest _request { get; set; }

        public AgregarServicioPruebaCommand(ServicioRequest request)
        {
            _request = request;
        }
    }
}
