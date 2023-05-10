namespace UCABPagaloTodoMS.Core.Entities;

public class ServicioEntity : BaseEntity
{
   public string? name { get; set; }
   public string? accountNumber { get; set; }
   public List<PagoEntity> Pago { get; set; }
}