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
    public class AsignarServicioComand : IRequest<string>
    {
        public Guid _prestadorServicioId { get; set; }
        public ServicioRequest _request { get; set; }

        public AsignarServicioComand(Guid prestadorServicioId, ServicioRequest ServicioRequests)
        {
            _request = ServicioRequests;
            _prestadorServicioId = prestadorServicioId;
        }
    }
  
}
