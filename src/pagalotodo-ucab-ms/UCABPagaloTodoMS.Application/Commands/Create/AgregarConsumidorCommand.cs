using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AgregarConsumidorCommand : IRequest<string>
    {
        public ConsumidorRequest _request { get; set; }

        public AgregarConsumidorCommand(ConsumidorRequest request)
        {
            _request = request;
        }
    }
}