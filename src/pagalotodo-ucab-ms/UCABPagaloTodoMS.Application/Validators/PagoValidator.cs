using FluentValidation;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Validators;

public class PagoValidator : AbstractValidator<PagoEntity>
{
    public PagoValidator()
    {
        RuleFor(pago => pago.consumidor).NotNull();
        RuleFor(pago => pago.servicio).NotNull();
        RuleFor(pago => pago.valor).NotNull();
    }
    
}