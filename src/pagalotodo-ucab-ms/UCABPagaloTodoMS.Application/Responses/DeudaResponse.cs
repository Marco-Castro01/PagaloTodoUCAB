using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Core.Entities;
[ExcludeFromCodeCoverage]
public class DeudaResponse 
{
    
    public Guid idDeuda { get; set; }
    public string identificador { get; set; }
    public Guid servicioId { get; set; }
   
    public string servicioName { get; set; }
    public double deuda { get; set; }
    public List<CamposPagosRequest> camposPagos { get; set; }
}