using UCABPagaloTodoWeb.enums;

namespace UCABPagaloTodoWeb.Models
{
    public class PagoModel
    {
        public Guid Id { get; set; }
        public double? valor { get; set; }
        public StatusPago? statusPago { get; set; }

        public Guid servicioId { get; set; }
        public string? NombreServicio { get; set; }
        public string? PrestadorServicioNombre { get; set; }
        public Guid consumidorId { get; set; }
        public string NombreConsumidor { get; set; }
    }
}
