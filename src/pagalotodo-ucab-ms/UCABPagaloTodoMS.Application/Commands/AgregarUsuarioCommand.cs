using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AgregarUsuarioCommand : IRequest<Guid>
    {
        public UsuarioRequest _request { get; set; }

        public AgregarUsuarioCommand(UsuarioRequest request)
        {
            _request = request;
        }
    }
  
}
