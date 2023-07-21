using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Application.Responses
{
    [ExcludeFromCodeCoverage]
    public class ErrorResponse 
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
