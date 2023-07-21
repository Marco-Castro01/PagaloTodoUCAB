using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Responses
{
    [ExcludeFromCodeCoverage]
    public class ServicioResponse
    {
        public Guid Id { get; set; }
        public string? name { get; set; }
        public string? accountNumber { get; set; }
        public List<CamposPagosRequest>? CamposDeLosPagos { get; set; }
        
        public Guid prestadorServicioId { get; set; }
        public string? prestadorServicioName { get; set; }
        public TipoServicio tipoServicio  { get; set; }
        public StatusServicio statusServicio { get; set; }
        
    }
}