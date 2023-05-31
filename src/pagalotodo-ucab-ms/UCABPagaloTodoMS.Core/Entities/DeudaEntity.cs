namespace UCABPagaloTodoMS.Core.Entities;

public class DeudaEntity : BaseEntity
{
    public string identificador { get; set; }
    public ServicioEntity? servicio { get; set; }
    public double deuda { get; set; }
}