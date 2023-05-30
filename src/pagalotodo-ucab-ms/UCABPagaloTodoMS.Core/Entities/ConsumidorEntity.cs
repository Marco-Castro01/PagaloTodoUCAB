namespace UCABPagaloTodoMS.Core.Entities;

public class ConsumidorEntity : UsuarioEntity
{
    public string? lastName { get; set; }
    public List<PagoEntity>? pago { get; set; }

}