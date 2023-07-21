using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class DeleteUsuarioCommand : IRequest<string>
    {
        public Guid _idUsuario { get; set; }

        public DeleteUsuarioCommand(Guid idUsuario)
        {
            _idUsuario = idUsuario;
        }
    }
}
