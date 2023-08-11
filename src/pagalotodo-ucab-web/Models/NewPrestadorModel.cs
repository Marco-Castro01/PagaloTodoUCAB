namespace UCABPagaloTodoWeb.Models
{
    public class NewPrestadorModel
    {
        public string email { get; set; }

        public string password { get; set; }
        public List<ServicioModel>? servicios { get; set; }

        public string? name { get; set; }
        public string nickName { get; set; }
        public string? rif { get; set; }
    }
}
