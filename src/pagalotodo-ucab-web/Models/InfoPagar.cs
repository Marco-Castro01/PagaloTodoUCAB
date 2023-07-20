namespace UCABPagaloTodoWeb.Models
{
    public class InfoPagar
    {
        public Guid DeudaId { get; set; }
        public decimal ValorDeuda { get; set; }
        public List<string> CamposPagos { get; set; }


    }
}