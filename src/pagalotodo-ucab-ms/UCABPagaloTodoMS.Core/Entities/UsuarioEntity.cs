using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UCABPagaloTodoMS.Core.Entities;
[Index(nameof(name), IsUnique = true)]
[Index(nameof(cedula), IsUnique = true)]
[Index(nameof(nickName), IsUnique = true)]
public abstract class UsuarioEntity : BaseEntity
{
  
    public byte[] passwordHash { get; set; } = new byte[32];
    public byte[] passwordSalt { get; set; } = new byte[32];
  
    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    
    [EmailAddress]
    public string? email { get; set; }
    public string? name { get; set; }
    public string? cedula { get; set; }
    public string nickName { get; set; }
    public bool status { get; set; }
    public string? Discriminator;

}