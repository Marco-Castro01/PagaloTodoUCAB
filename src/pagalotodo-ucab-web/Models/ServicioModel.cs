
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoWeb.Models
{
    public class ServicioModel
    {
        public Guid Id { get; set; }
        public string? name { get; set; }
        public string? accountNumber { get; set; }
        
        public Guid prestadorServicioId { get; set; }
        public string? prestadorServicioName { get; set; }
        public TipoServicio tipoServicio  { get; set; }
        public StatusServicio statusServicio { get; set; }
        
    }
}