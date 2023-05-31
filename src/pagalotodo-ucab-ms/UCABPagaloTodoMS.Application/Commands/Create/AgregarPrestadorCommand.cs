using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AgregarPrestadorCommand : IRequest<Guid>
    {
        public PrestadorRequest _request { get; set; }

        public AgregarPrestadorCommand(PrestadorRequest request)
        {
            _request = request;
        }
    }

}
