using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    [ExcludeFromCodeCoverage]
    public class AgregarPagoPorverificacionCommand : IRequest<Guid>
    {
        public PagoVerificacionRequest _request { get; set; }
        public Guid _idConsumidor { get; set; }
        public Guid _idDeuda { get; set; }


        public AgregarPagoPorverificacionCommand(PagoVerificacionRequest request, Guid idConsumidor,Guid idDeuda)
        {
            _idDeuda = idDeuda;
            _request = request;
            _idConsumidor = idConsumidor;
        }
    }
}
