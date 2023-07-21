using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Responses
{
    [ExcludeFromCodeCoverage]
    public class PagoResponse
    {
        public Guid Id { get; set; }
        public double? valor { get; set; }
        public Guid servicioId { get; set; }
        public string? NombreServicio { get; set; }
        public string? PrestadorServicioNombre { get; set; }
        public Guid consumidorId { get; set; }
        public string NombreConsumidor { get; set; }
    }
}
