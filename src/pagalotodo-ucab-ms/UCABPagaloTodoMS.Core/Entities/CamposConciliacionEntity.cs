using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Core.Entities;
[ExcludeFromCodeCoverage]
public class CamposConciliacionEntity : BaseEntity
{
    public string? Nombre { get; set; }
    public int Longitud { get; set; }
    public List<ServicioCampoEntity>? ServicioCampo { get; set; }
    public List<ServicioEntity> Servicio { get; set; }
}