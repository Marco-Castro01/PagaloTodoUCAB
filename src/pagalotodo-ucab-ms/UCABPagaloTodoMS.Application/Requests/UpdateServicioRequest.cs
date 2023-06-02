using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class UpdateServicioRequest
    {
        public Guid idServicio { get; set; }
        public string? name { get; set; }
        public string? accountNumber { get; set; }
        public TipoServicio tipoServicio { get; set; }
        public StatusServicio statusServicio { get; set; }
    }
}
