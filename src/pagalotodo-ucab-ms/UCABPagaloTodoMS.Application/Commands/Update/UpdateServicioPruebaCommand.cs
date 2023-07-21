using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    [ExcludeFromCodeCoverage]
    public class UpdateServicioPruebaCommand : IRequest<string>
    {
        public UpdateServicioRequest _request { get; set; }
        public Guid _idServicio { get; set; }
        public UpdateServicioPruebaCommand(Guid idServicio,UpdateServicioRequest request)
        {
            _idServicio = idServicio;
            _request = request;
        }
    }
}
