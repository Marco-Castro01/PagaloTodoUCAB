namespace UCABPagaloTodoWeb.Models
{
    public class DeudaModel
    {

        public Guid idDeuda { get; set; }
        public string identificador { get; set; }
        public Guid servicioId { get; set; }

        public string servicioName { get; set; }
        public double deuda { get; set; }
        public List<CamposPagosModel> camposPagos { get; set; }
    }
}
