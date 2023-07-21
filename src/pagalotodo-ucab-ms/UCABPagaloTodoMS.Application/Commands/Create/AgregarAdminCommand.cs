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
    public class AgregarAdminCommand : IRequest<Guid>
    {
        public AdminRequest _request { get; set; }

        public AgregarAdminCommand(AdminRequest request)
        {
            _request = request;
        }
    }
  
}
