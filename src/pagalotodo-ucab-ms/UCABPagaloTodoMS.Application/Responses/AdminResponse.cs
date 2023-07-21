using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class AdminResponse
    {
        [ExcludeFromCodeCoverage]
        public Guid Id { get; set; }
        public string? email { get; set; } 
        public string? password { get; set; }
        public string? nickName { get; set; }
        public bool status { get; set; }
        public string? cedula { get; set; } 

    }
}
