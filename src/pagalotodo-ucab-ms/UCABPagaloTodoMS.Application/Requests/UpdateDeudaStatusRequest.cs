using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class UpdateDeudaStatusRequest
    {
        [ExcludeFromCodeCoverage]
        public Guid idDeuda { get; set; }
   
        public bool deudaStatus { get; set; }
    }
}
