using System.Text.RegularExpressions;
using FluentValidation;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Core.Enums;

public class CampoEnPagoValidator : AbstractValidator<CamposPagosRequest>
{
    public CampoEnPagoValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage("Debe tener un nombre de campo");
        RuleFor(x => x.Longitud).GreaterThan(0).WithMessage("La longitud debe ser mayor a 0");

        RuleFor(x => x.formatofecha)
            .Must((request, formatofecha) =>
            {
                if (request.TipoDato == TipoDato.fecha)
                {
                    // Validar el formato de fecha aquí
                    return Regex.IsMatch(formatofecha, @"^(?:(?:(?:0?[1-9]|1\d|2[0-8])([\/.-])(?:0?[1-9]|1[0-2])\1(?:\d{4}|\d{2}))|(?:(?:(?:0?[1-9]|1[0-2])([\/.-])(?:0?[1-9]|1\d|2[0-8])\2(?:\d{4}|\d{2}))|(?:(?:\d{4}|\d{2})([\/.-])(?:0?[1-9]|1[0-2])\3(?:0?[1-9]|1\d|2[0-8]))))$");
                }
                return true;
            })
            .WithMessage("El formato de fecha no es válido.")
            .When(x => x.formatofecha != null);
        RuleFor(x => x.contenido).NotEmpty().NotNull().WithMessage("Contenido Vacio");
        RuleFor(x => x.TipoDato).Must((request, tipoDato) =>
        {
            if (tipoDato == TipoDato.entero)
            {
                return request.formatofecha == null &&
                       request.separadorDeDecimales == null &&
                       (request.separadorDeMiles == "," || request.separadorDeMiles == ".");
            }
            else if (tipoDato == TipoDato.conDecimal)
            {
                return request.formatofecha == null &&
                       (request.separadorDeMiles == "," || request.separadorDeMiles == ".") &&
                       (request.separadorDeDecimales == "," || request.separadorDeDecimales == ".") &&
                       request.separadorDeMiles != request.separadorDeDecimales;
            }
            else if (tipoDato == TipoDato.hiperTexto)
            {
                return request.formatofecha == null &&
                       request.separadorDeMiles == null &&
                       request.separadorDeDecimales == null;
            }

            return false;
        }).WithMessage("Las propiedades no cumplen las condiciones requeridas para el TipoDato especificado.");
    }
}
