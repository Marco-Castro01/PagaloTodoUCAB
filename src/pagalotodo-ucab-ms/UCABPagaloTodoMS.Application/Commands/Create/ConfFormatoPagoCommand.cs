using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class ConfFormatoPagoCommand : IRequest<Guid>
    {
        public Guid _ServicioId { get; set; }
        public List<CamposPagosRequest> _ListaCamposPagos { get; set; }
        public ConfFormatoPagoCommand(Guid servicioId, List<CamposPagosRequest> camposPagos)
        {
            _ServicioId = servicioId;
            _ListaCamposPagos = camposPagos;
        }
    }
  
}
