using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class CamposConciliacionResponse
    {
        [ExcludeFromCodeCoverage]
        public Guid Id { get; set; }
        public string? Nombre { get; set; } 
        public int? Longitud { get; set; }

    }
}
