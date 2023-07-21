using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AgregarPrestadorServicioPruebaCommand : IRequest<Guid>
    {
        public PrestadorServicioRequest _request { get; set; }

        public AgregarPrestadorServicioPruebaCommand(PrestadorServicioRequest request)
        {
            _request = request;
        }
    }
}
