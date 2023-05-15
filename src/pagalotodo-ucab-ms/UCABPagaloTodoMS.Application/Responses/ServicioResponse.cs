using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class ServicioResponse
    {
        public Guid Id { get; set; }
        public string? name { get; set; }
        public string? accountNumber { get; set; }
        public PrestadorServicioEntity PrestadorServicioId { get; set; }
    }
}
