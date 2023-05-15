using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class PagoResponse
    {
        public Guid Id { get; set; }
        public double? valor { get; set; }
        public ServicioEntity servicioId { get; set; }
        public ConsumidorEntity consumidorId { get; set; }
    }
}
