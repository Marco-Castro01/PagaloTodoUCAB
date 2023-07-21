using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    [ExcludeFromCodeCoverage]
    public class AgregarPrestadorServicioPruebaCommand : IRequest<Guid>
    {
        public PrestadorServicioRequest _request { get; set; }

        public AgregarPrestadorServicioPruebaCommand(PrestadorServicioRequest request)
        {
            _request = request;
        }
    }
}
