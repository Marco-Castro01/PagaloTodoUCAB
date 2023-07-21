using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Responses
{
    [ExcludeFromCodeCoverage]
    public class PrestadorServicioResponse
    {
        public Guid Id { get; set; }
        public string? email { get; set; } 
        public string? nickName { get; set; }
        public List<ServicioEntity>? servicios { get; set; }
        public bool status { get; set; }
        public string? rif { get; set; } 

    }
}
