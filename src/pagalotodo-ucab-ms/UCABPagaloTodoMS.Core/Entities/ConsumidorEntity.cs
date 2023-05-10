namespace UCABPagaloTodoMS.Core.Entities;

public class ConsumidorEntity : UsuarioEntity
{
    public string? name { get; set; }
    public string? lastName { get; set; }
    public string? cedula { get; set; }
    public List<PagoEntity>? pago { get; set; }

}