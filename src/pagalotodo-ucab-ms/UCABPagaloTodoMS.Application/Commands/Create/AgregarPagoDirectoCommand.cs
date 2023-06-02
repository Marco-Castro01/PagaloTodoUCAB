using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AgregarPagoDirectoCommand : IRequest<Guid>
    {
        public PagoDirectoRequest DirectoRequest { get; set; }

        public AgregarPagoDirectoCommand(PagoDirectoRequest directoRequest)
        {
            DirectoRequest = directoRequest;
        }
    }
}
