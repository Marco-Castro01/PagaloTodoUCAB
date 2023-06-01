using Azure.Messaging.ServiceBus.Administration;
using FluentValidation;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Validators;

public class PagoDeudaValidator : AbstractValidator<DeudaResponse>
{
    public PagoDeudaValidator()
    {
        RuleFor(deuda => deuda.identificador).NotNull().WithMessage("No tiene Deuda");
        RuleFor(deuda => deuda.deuda).NotNull().WithMessage("Monto Invalido");
        RuleFor(deuda => deuda.deudaStatus).NotNull().Must(x => x == false).WithMessage("Ya pagado");
        RuleFor(deuda => deuda.deleted).NotNull().Must(x => x == false).WithMessage("Inexistente");
        RuleFor(deuda => deuda.servicio).NotNull().WithMessage("Error Conectando Servicio");
        RuleFor(deuda => deuda.servicio.tipoServicio).NotNull().Must(x => x == TipoServicio.validacion).WithMessage("Accion invalida Tipo servicio");

        
    }
}