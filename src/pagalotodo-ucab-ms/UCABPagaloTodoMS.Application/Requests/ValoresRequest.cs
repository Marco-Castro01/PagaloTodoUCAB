using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class ValoresRequest
    {
        [ExcludeFromCodeCoverage]
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Identificacion { get; set;}
    }
}
