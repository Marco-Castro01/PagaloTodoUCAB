namespace UCABPagaloTodoMS.Core.Entities;

public class UsuarioEntity : BaseEntity
{
    public string email { get; set; } 
    public string password { get; set; }
    public string nickName { get; set; }
    public bool status { get; set; }
}