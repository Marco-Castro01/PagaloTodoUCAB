namespace UCABPagaloTodoWeb.Models
{
    public class UsuariosModel {

        public Guid Id { get; set; }
        public string? email { get; set; }
        public string? name { get; set; }
        public string? cedula { get; set; }
        public string? rif { get; set; }
        public string nickName { get; set; }
        public bool status { get; set; }
        public string? Discriminator { get; set; }
    }
}
