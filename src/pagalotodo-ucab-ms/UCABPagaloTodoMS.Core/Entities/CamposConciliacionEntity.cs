namespace UCABPagaloTodoMS.Core.Entities;

public class CamposConciliacionEntity : BaseEntity
{
   public string Nombre { get; set; }
   public int Longitud { get; set; }
   public List<ServicioEntity> Servicio { get; set; }
}