using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class PrestadorServicioRequest
    {
        public string? email { get; set; } 
        public string? password { get; set; }
        public string? nickName { get; set; }
        public bool status { get; set; }
        public string? rif { get; set; } 
    }
}
