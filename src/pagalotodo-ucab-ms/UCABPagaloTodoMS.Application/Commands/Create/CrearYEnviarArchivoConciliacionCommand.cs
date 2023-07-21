using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    [ExcludeFromCodeCoverage]
    public class CrearYEnviarArchivoConciliacionCommand : IRequest<string>
    {
        public Guid _idprestadorservicio { get; set; }

        public CrearYEnviarArchivoConciliacionCommand(Guid idprestadorservicio)
        {
            _idprestadorservicio = idprestadorservicio;
        }
    }
}
