namespace UCABPagaloTodoMS.Core.Entities;

public class DeudaResponse 
{
    public string identificador { get; set; }
    public Guid servicioId { get; set; }
    public string servicioName { get; set; }
    public double deuda { get; set; }
}