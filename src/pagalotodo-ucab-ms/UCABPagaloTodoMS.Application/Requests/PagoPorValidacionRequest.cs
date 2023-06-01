using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class PagoPorValidacionRequest
    {
        public Guid IdDeuda { get; set; }
        public Guid ServicioId { get; set; }
        public Guid ConsumidorId { get; set; }
    }
}
