using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using FluentValidation;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Core.Enums;

public class CampoEnPagoValidator : AbstractValidator<CamposPagosRequest>
{
    [ExcludeFromCodeCoverage]
    public CampoEnPagoValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage("Debe tener un nombre de campo");
        RuleFor(x => x.Longitud).Must((request, longitud) =>
        {
            if (request.TipoDato == TipoDato.fecha)
            {
                return longitud == 0;
            }
            else
            {
                return longitud > 0;
            }
        }).WithMessage(request => request.TipoDato == TipoDato.fecha ? "La longitud debe ser 0 para el TipoDato Fecha." : "La longitud debe ser mayor a 0 para otros tipos de dato.");

        RuleFor(x => x.contenido)
            .Must((request, contenido) =>
            {
                if (request.TipoDato == TipoDato.fecha)
                {
                    // Validar el formato de fecha aquí
                    return Regex.IsMatch(contenido, @"^(?:(?:(?:0?[1-9]|1\d|2[0-8])([\/.-])(?:0?[1-9]|1[0-2])\1(?:\d{4}|\d{2}))|(?:(?:(?:0?[1-9]|1[0-2])([\/.-])(?:0?[1-9]|1\d|2[0-8])\2(?:\d{4}|\d{2}))|(?:(?:\d{4}|\d{2})([\/.-])(?:0?[1-9]|1[0-2])\3(?:0?[1-9]|1\d|2[0-8]))))$");
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
                       (request.separadorDeMiles == "," || request.separadorDeMiles == ".") &&
                       request.contenido.All(char.IsDigit);


            }
            else if (tipoDato == TipoDato.conDecimal)
            {
                return request.formatofecha == null &&
                       (request.separadorDeMiles == "," || request.separadorDeMiles == ".") &&
                       (request.separadorDeDecimales == "," || request.separadorDeDecimales == ".") &&
                       request.separadorDeMiles != request.separadorDeDecimales &&
                       Regex.IsMatch(request.contenido, @"^[0-9]+(\.[0-9]+)?$");
            }
            else if (tipoDato == TipoDato.hiperTexto)
            {
                return request.formatofecha != null &&
                       request.separadorDeMiles == null &&
                       request.separadorDeDecimales == null;

            }else if (tipoDato==TipoDato.fecha)
            {
                return request.contenido != null &&
                       request.formatofecha != null &&
                       request.separadorDeMiles == null &&
                       request.separadorDeDecimales == null &&
                       request.Longitud == 0;


            }

            return false;
        }).WithMessage("Las propiedades no cumplen las condiciones requeridas para el TipoDato especificado.");
    }
}
