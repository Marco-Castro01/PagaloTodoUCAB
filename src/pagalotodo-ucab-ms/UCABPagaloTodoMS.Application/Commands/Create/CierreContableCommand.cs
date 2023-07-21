using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    [ExcludeFromCodeCoverage]
    public class CierreContableCommand : IRequest<List<CierreContableResponse>>
    {
        public Guid _idprestadorservicio { get; set; }

        public CierreContableCommand(Guid idprestadorservicio)
        {
            _idprestadorservicio = idprestadorservicio;
        }
    }
}
