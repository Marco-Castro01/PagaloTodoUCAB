using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    [ExcludeFromCodeCoverage]
    public class DeleteServicioPruebaCommand : IRequest<Guid>
    {
        public Guid _idServicio { get; set; }

        public DeleteServicioPruebaCommand(Guid idServicio)
        {
            _idServicio = idServicio;
        }
    }
}
