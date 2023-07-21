using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class UpdateCampoCommand : IRequest<string>
    {
        public CamposConciliacionRequest _request { get; set; }
        public Guid _idCampo { get; set; }

        public UpdateCampoCommand(CamposConciliacionRequest request,Guid idCampo)
        {
            _request = request;
            _idCampo = idCampo;
        }
    }
}
