using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    [ExcludeFromCodeCoverage]
    public class AgregarValorPruebaCommand : IRequest<Guid>
    {
        public ValoresRequest _request { get; set; }

        public AgregarValorPruebaCommand(ValoresRequest request)
        {
            _request = request;
        }
    }
}
