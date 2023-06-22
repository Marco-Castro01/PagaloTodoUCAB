namespace UCABPagaloTodoMS.Core.Entities;

public class ArchivoFirebaseEntity : BaseEntity
{
    public string urlFirebase { get; set; }
    public PrestadorServicioEntity prestadorServicio { get; set; }
}