using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AgregarCamposConciliacionPruebaCommand : IRequest<Guid>
    {
        public CamposConciliacionRequest _request { get; set; }

        public AgregarCamposConciliacionPruebaCommand(CamposConciliacionRequest request)
        {
            _request = request;
        }
    }
}
