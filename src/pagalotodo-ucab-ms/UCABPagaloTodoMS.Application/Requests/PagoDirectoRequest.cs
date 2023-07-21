using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class PagoDirectoRequest
    {
        public double? Valor { get; set; }

        public List<CamposPagosRequest> camposPagos { get; set; }
    }
}
