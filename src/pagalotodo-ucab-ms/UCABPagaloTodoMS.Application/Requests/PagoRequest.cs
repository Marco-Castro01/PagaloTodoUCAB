using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class PagoRequest
    {
        public double? Valor { get; set; }
        public Guid ServicioId { get; set; }
        public Guid ConsumidorId { get; set; }
    }
}
