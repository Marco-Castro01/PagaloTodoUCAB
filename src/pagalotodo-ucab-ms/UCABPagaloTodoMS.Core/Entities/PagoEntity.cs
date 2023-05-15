namespace UCABPagaloTodoMS.Core.Entities;

public class PagoEntity : BaseEntity
{
    public double? valor { get; set; }

    public ServicioEntity servicio { get; set; }
    public ServicioEntity consumidor { get; set; }
}