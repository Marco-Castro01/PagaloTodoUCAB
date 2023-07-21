using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Commands
{
    [ExcludeFromCodeCoverage]
    public class EditarUsuarioCommand : IRequest<Guid>

    {
        public EditarUsuarioRequest _request { get; set; }
        public Guid _id { get; set; }

        public EditarUsuarioCommand(EditarUsuarioRequest request, Guid id)
        {
            _request = request;
            _id = id;
        }
    }
}