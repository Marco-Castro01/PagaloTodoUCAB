using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class CambiarContrasenaCommand : IRequest<Guid>
    {
        public ChangePasswordRequest _request { get; set; }

        public CambiarContrasenaCommand(ChangePasswordRequest request)
        {
            _request = request;
        }
    }
}
