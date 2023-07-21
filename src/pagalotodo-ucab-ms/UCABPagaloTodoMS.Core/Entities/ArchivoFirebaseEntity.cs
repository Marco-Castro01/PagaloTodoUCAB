using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Core.Entities;
[ExcludeFromCodeCoverage]
public class ArchivoFirebaseEntity : BaseEntity
{
    public string urlFirebase { get; set; }
    public PrestadorServicioEntity prestadorServicio { get; set; }
    public ArchivoFirebase tipoArchivo { get; set; }
}