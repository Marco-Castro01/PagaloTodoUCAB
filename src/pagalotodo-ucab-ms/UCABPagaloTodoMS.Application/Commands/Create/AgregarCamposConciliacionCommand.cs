using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    [ExcludeFromCodeCoverage]
    public class AgregarCamposConciliacionCommand : IRequest<Guid>
    {
        public CamposConciliacionRequest _request { get; set; }

        public AgregarCamposConciliacionCommand(CamposConciliacionRequest request)
        {
            _request = request;
        }
    }
}
