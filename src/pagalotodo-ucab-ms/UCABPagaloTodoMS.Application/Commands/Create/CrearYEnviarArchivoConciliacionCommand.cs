using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class CrearYEnviarArchivoConciliacionCommand : IRequest<string>
    {
        public Guid _idprestadorservicio { get; set; }

        public CrearYEnviarArchivoConciliacionCommand(Guid idprestadorservicio)
        {
            _idprestadorservicio = idprestadorservicio;
        }
    }
}
