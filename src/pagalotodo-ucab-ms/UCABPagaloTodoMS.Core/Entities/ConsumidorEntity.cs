using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Core.Entities;

public class ConsumidorEntity : UsuarioEntity
{
    [ExcludeFromCodeCoverage]
    public string? lastName { get; set; }
    public List<PagoEntity>? pago { get; set; }

}