using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class DeleteServicioRequest
    {
        public Guid idServicio { get; set; }
        public bool delete { get; set; }
       
    }
}
