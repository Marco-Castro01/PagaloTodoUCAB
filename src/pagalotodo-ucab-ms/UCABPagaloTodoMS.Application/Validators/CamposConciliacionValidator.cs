using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Validators;
[ExcludeFromCodeCoverage]
public class CamposConciliacionValidator : AbstractValidator<CamposConciliacionEntity>
{

    public CamposConciliacionValidator()
    {
        RuleFor(campo => campo.Longitud).NotNull()
            .WithMessage("El campo Longitud debe ser un número válido.");;
        RuleFor(campo => campo.Nombre).NotNull().WithMessage("Error en Nombre");


    }
    
}


