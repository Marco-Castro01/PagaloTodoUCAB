using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class CierreContableCommand : IRequest<List<CierreContableResponse>>
    {
        public Guid _idprestadorservicio { get; set; }

        public CierreContableCommand(Guid idprestadorservicio)
        {
            _idprestadorservicio = idprestadorservicio;
        }
    }
}
