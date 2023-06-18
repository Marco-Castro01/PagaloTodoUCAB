using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AgregarCamposConciliacionCommand : IRequest<Guid>
    {
        public CamposConciliacionRequest _request { get; set; }

        public AgregarCamposConciliacionCommand(CamposConciliacionRequest request)
        {
            _request = request;
        }
    }
}
