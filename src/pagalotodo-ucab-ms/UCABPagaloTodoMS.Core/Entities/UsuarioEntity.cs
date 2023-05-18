using System.ComponentModel.DataAnnotations;

namespace UCABPagaloTodoMS.Core.Entities;

public abstract class UsuarioEntity : BaseEntity
{
  
    public byte[] passwordHash { get; set; } = new byte[32];
    public byte[] passwordSalt { get; set; } = new byte[32];
    public string? VerificationToken { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public string? email { get; set; }
    public string? name { get; set; }
    public string? cedula { get; set; }
    public string nickName { get; set; }
    public bool status { get; set; }
    public string? Discriminator;

}