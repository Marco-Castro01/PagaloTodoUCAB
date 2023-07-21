using System.ComponentModel.DataAnnotations;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Core.Entities;

public class PagoEntity : BaseEntity
{
    public double? valor { get; set; }
    public virtual ServicioEntity? servicio { get; set; }
    public virtual ConsumidorEntity? consumidor { get; set; }
    public StatusPago status { get; set; } = StatusPago.enEspera;
    public string? formatoPago { get; set; }
}