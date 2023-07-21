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
    public class AgregarConsumidorCommand : IRequest<Guid>
    {
        public ConsumidorRequest _request { get; set; }

        public AgregarConsumidorCommand(ConsumidorRequest request)
        {
            _request = request;
        }
    }
}