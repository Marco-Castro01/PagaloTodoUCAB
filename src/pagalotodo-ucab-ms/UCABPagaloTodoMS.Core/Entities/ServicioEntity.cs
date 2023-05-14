namespace UCABPagaloTodoMS.Core.Entities;

public class ServicioEntity : BaseEntity
{
   public string? name { get; set; }
   public string? accountNumber { get; set; }
   public List<PagoEntity> Pago { get; set; }
   public List<CamposConciliacionEntity> CamposConciliacion { get; set; }
   public PrestadorServicioEntity PrestadorServicio { get; set; }
}