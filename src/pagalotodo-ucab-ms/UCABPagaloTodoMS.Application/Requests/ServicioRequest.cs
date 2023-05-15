namespace UCABPagaloTodoMS.Application.Requests
{
    public class ServicioRequest
    {
        public string? name { get; set; }
        public string? accountNumber { get; set; }
        public Guid PrestadorServicioId { get; set; }
    }
}
