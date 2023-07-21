using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Application.Responses
{
    [ExcludeFromCodeCoverage]
    public class ValoresResponse
    {
        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public string? Identificacion { get; set; }
    }
}
