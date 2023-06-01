using Azure.Messaging.ServiceBus.Administration;
using FluentValidation;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Validators;

public class DeudaValidator : AbstractValidator<DeudaEntity>
{
    public DeudaValidator()
    {
        RuleFor(deuda => deuda.identificador).NotNull().WithMessage("No tiene Deuda").DependentRules(() =>
        {
            RuleFor(deuda => deuda.deuda).NotNull().WithMessage("Monto Invalido");
            RuleFor(deuda => deuda.deudaStatus).NotNull().WithMessage("Error eN ESTATUS");
            RuleFor(deuda => deuda.servicio).NotNull().WithMessage("Error al conectar con el servicio");
            
        });
        
    }
}