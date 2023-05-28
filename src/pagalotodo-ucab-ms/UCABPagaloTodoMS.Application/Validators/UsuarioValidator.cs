using FluentValidation;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Validators;

public class UsuarioValidator : AbstractValidator<UsuarioEntity>
{

    public UsuarioValidator()
    {
        RuleFor(usuario => usuario.name).NotNull();
        RuleFor(usuario => usuario.cedula).NotNull();
        RuleFor(usuario => usuario.nickName).NotNull();
        RuleFor(usuario => usuario.email).NotNull().EmailAddress();
        
    }
    
}


