using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AgregarPagoPorValidacionCommand : IRequest<Guid>
    {
        public PagoPorValidacionRequest Request { get; set; }

        public AgregarPagoPorValidacionCommand(PagoPorValidacionRequest request)
        {
            Request = request;
        }
    }
}
