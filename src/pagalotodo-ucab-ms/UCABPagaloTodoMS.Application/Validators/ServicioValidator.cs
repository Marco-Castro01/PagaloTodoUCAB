using FluentValidation;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Validators;

public class ServicioValidator : AbstractValidator<ServicioEntity>
{
    public ServicioValidator()
    {
        RuleFor(servicio=> servicio.name).NotNull();
        RuleFor(servicio => servicio.accountNumber).NotNull();
        RuleFor(servicio => servicio.tipoServicio)
            .NotNull()
            .Must(x =>  (x == TipoServicio.directo) 
                        || (x== TipoServicio.validacion)
                        );
        RuleFor(servicio => servicio.statusServicio)
            .NotNull()
            .Must(x =>  (x == StatusServicio.activa) 
                        || (x == StatusServicio.inactiva)
                        ||(x == StatusServicio.proximamente)
                        
                        );

    }
}