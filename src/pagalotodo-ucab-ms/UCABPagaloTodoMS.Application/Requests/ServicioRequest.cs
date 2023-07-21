using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class ServicioRequest
    {
        public string? name { get; set; }
        public string? accountNumber { get; set; }
        public TipoServicio tipoServicio { get; set; }
        public StatusServicio statusServicio { get; set; }
    }
}
