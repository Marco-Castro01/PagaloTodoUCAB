using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class PagoDirectoRequest
    {
        public double? Valor { get; set; }

        public List<CamposPagosRequest> camposPagos { get; set; }
    }
}
