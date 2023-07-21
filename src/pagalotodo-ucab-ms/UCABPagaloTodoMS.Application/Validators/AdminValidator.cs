using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Validators;

public class AdminValidator : AbstractValidator<AdminEntity>
{
    [ExcludeFromCodeCoverage]
    public AdminValidator()
    {
        RuleFor(usuario => usuario.name).NotNull();
        RuleFor(usuario => usuario.cedula).NotNull();
        RuleFor(usuario => usuario.nickName).NotNull();
        RuleFor(usuario => usuario.email).NotNull().EmailAddress();
        
    }
    
}


