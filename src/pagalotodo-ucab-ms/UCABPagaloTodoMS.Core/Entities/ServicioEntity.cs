using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Core.Entities;
[ExcludeFromCodeCoverage]
public class ServicioEntity : BaseEntity
{
  
    public string? name { get; set; }
    public string? accountNumber { get; set; }
    public List<PagoEntity>? Pago { get; set; }
    public List<ServicioCampoEntity>? ServicioCampo { get; set; }
    public PrestadorServicioEntity PrestadorServicio { get; set; }
    public List<DeudaEntity>? deudas { get; set; }
    public TipoServicio tipoServicio  { get; set; }
    public StatusServicio statusServicio { get; set; }
    public string? formatoDePagos { get; set; }
    public List<CamposConciliacionEntity> CamposConciliacion { get; set; }
}