using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class PagoVerificacionRequest
    {
        public double? Valor { get; set; }
        public List<CamposPagosRequest> camposPagos { get; set; }
    }
}
