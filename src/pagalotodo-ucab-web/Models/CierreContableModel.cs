namespace UCABPagaloTodoWeb.Models
{
    public class CierreContableModel
    {
        public Guid idPrestador { get; set; }
        public string prestadorName { get; set; }
        public Guid idServicio { get; set; }
        public string servicioName { get; set; }
        public double? total { get; set; }
        public string? archivoURL { get; set; }

    }
}
