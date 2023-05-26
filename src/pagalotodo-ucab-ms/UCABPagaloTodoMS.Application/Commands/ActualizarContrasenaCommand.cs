using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class ActualizarContrasenaCommand : IRequest<Guid>
    {
        public UpdatePasswordRequest _request { get; set; }

        public ActualizarContrasenaCommand(UpdatePasswordRequest request)
        {
            _request = request;
        }
    }
}
