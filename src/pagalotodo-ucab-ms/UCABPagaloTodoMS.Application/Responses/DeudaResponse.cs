namespace UCABPagaloTodoMS.Core.Entities;

public class DeudaResponse 
{
    
    public Guid idDeuda { get; set; }
    public string identificador { get; set; }
    public Guid servicioId { get; set; }
    public string servicioName { get; set; }
    public double deuda { get; set; }
}