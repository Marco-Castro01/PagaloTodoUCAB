using System.Text;
using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.AspNetCore.Http;
namespace UCABPagaloTodoMS.Application.Commands
{
    

    public class RecibirArchivoConciliacionCommand : IRequest<string>
    {
        
        public IFormFile _file { get; set; }
        
        public Guid _idprestadorservicio { get; set; }

        public RecibirArchivoConciliacionCommand(IFormFile file,Guid idprestadorservicio)
        {
            _idprestadorservicio = idprestadorservicio;
            _file = file;
        }
    }
}
