using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Validators
{
    [ExcludeFromCodeCoverage]
    public class ConsumidorValidator : AbstractValidator<ConsumidorEntity>
    {
        public ConsumidorValidator()
        {
            RuleFor(usuario => usuario.name).NotNull();
            RuleFor(usuario => usuario.cedula).NotNull();
            RuleFor(usuario => usuario.nickName).NotNull();
            RuleFor(usuario => usuario.email).NotNull().EmailAddress();

        }
    }
}
