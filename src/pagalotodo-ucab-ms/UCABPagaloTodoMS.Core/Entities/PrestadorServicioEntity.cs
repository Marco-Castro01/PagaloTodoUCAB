using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Core.Entities;
[ExcludeFromCodeCoverage]
public class PrestadorServicioEntity : UsuarioEntity
{
    public string? rif { get; set; }
    public List<ServicioEntity>? Servicio { get; set; }
    public List<ArchivoFirebaseEntity>? ArchivoFirebase { get; set; }

}