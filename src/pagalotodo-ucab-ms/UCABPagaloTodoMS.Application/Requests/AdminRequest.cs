using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class AdminRequest
    {
        [ExcludeFromCodeCoverage]
        public string? email { get; set; }
        public string? name { get; set; }
        public string? password { get; set; }
        public string? nickName { get; set; }
        public string? cedula { get; set; } 
    }
}
