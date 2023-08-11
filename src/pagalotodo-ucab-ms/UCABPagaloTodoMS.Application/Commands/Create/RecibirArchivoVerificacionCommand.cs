using System.Text;
using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.AspNetCore.Http;
namespace UCABPagaloTodoMS.Application.Commands
{
    

    public class RecibirArchivoVerificacionCommand : IRequest<string>
    {
        
        public IFormFile _file { get; set; }
        
        public Guid _idServicio { get; set; }
        public Guid _IdPrestador { get; set; }

        public RecibirArchivoVerificacionCommand(IFormFile file,Guid idServicio,Guid prestadorId)
        {
            _idServicio = idServicio;
            _file = file;
            _IdPrestador = prestadorId;
        }
    }
}
