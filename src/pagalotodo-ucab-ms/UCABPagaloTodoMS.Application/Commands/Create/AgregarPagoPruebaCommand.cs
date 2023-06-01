using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AgregarPagoPruebaCommand : IRequest<Guid>
    {
        public PagoRequest _request { get; set; }

        public AgregarPagoPruebaCommand(PagoRequest request)
        {
            _request = request;
        }
    }
}
