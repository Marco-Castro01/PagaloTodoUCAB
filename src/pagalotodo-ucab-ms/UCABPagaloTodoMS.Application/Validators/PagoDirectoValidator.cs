using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Validators;
[ExcludeFromCodeCoverage]
public class PagoDirectoValidator : AbstractValidator<PagoEntity>
{
    public PagoDirectoValidator()
    {
        RuleFor(pago => pago.consumidor).NotNull();
        RuleFor(pago => pago.servicio).NotNull().WithMessage("Servicio inexistente");
        RuleFor(pago => pago.valor).NotNull().WithMessage("Valor Invalido");
        RuleFor(pago => pago.servicio!.tipoServicio)
            .NotNull()
            .Must(x =>  (x == TipoServicio.directo)).WithMessage("Accion no permitida");
        RuleFor(pago => pago.servicio!.statusServicio)
            .NotNull()
            .Must(x=>(x==StatusServicio.activa)).WithMessage("Accion no permitida, intente cuando el servicio este activo");
        RuleFor(pago => pago.servicio!.deleted)
            .NotNull()
            .Must(x => (x == false)).WithMessage("Servicio no existente");
    }
    
}