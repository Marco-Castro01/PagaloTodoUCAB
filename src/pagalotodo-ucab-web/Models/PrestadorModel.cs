namespace UCABPagaloTodoWeb.Models
{
    public class PrestadorModel {

        public Guid Id { get; set; }
        public string? email { get; set; }
        public string? name { get; set; }
        public string? rif { get; set; }
        public List<ServicioModel>? servicios { get; set; }
        public string nickName { get; set; }
        public bool status { get; set; }
        public List<CierreContableModel>? cierres { get; set; }
    }
}
