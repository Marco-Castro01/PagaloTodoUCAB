using System.ComponentModel.DataAnnotations;

namespace UCABPagaloTodoMS.Core.Entities;

public class PagoEntity : BaseEntity
{
    public double? valor { get; set; }

    [Required]
    public virtual ServicioEntity? servicio { get; set; }
    [Required]
    public virtual ConsumidorEntity? consumidor { get; set; }
}