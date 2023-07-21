using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    [ExcludeFromCodeCoverage]
    public class AgregarPagoDirectoCommand : IRequest<Guid>
    {
        public PagoDirectoRequest DirectoRequest { get; set; }
        public Guid _idServicio { get; set; }
        public Guid _idConsumidor { get; set; }

        public AgregarPagoDirectoCommand(PagoDirectoRequest directoRequest, Guid idServicio,Guid idConsumidor)
        {
            DirectoRequest = directoRequest;
            _idServicio = idServicio;
            _idConsumidor = idConsumidor;
        }
    }
}
