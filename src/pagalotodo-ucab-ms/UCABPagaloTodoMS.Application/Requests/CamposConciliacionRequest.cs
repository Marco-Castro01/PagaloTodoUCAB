using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class CamposConciliacionRequest
    {
        [ExcludeFromCodeCoverage]
        public string? Nombre { get; set; }
        public int Longitud { get; set; }
    }
}
