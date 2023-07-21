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
    public class PrestadorValidator : AbstractValidator<PrestadorServicioEntity>
    {
        public PrestadorValidator()
        {
            RuleFor(usuario => usuario.name).NotNull();
            RuleFor(usuario => usuario.rif).NotNull();
            RuleFor(usuario => usuario.nickName).NotNull();
            RuleFor(usuario => usuario.email).NotNull().EmailAddress();

        }
    }
}
