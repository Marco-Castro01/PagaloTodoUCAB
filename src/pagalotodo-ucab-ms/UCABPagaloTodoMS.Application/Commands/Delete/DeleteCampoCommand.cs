using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class DeleteCampoCommand : IRequest<string>
    {
        public Guid _idCampo { get; set; }

        public DeleteCampoCommand(Guid idCampo)
        {
            _idCampo = idCampo;
        }
    }
}
