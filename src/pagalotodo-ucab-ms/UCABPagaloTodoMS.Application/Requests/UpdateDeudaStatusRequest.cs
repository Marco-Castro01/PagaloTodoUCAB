namespace UCABPagaloTodoMS.Application.Requests
{
    public class UpdateDeudaStatusRequest
    {
        public Guid idDeuda { get; set; }
   
        public bool deudaStatus { get; set; }
    }
}
