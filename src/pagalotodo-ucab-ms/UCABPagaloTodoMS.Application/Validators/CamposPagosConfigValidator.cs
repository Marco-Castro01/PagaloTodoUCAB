using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using FluentValidation;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Core.Enums;

public class CamposPagosValidator : AbstractValidator<CamposPagosRequest>
{
    [ExcludeFromCodeCoverage]
    public CamposPagosValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage("Debe tener un nombre de campo");
        RuleFor(x => x.Longitud).Must((request, longitud) =>
        {
            if (request.TipoDato == TipoDato.hiperTexto)
            {
                return longitud > 0;

            }
            else
            {
                return longitud == 0; 
            }
        }).WithMessage(request => request.TipoDato == TipoDato.fecha ? "La longitud debe ser 0 para el TipoDato Fecha." : "La longitud debe ser mayor a 0 para otros tipos de dato.");

        RuleFor(x => x.formatofecha)
            .Must((request, formatofecha) =>
            {
                if (request.TipoDato == TipoDato.fecha)
                {
                    // Validar el formato de fecha aquí
                    return Regex.IsMatch(formatofecha,@"^(?:(?:dd\/mm\/yyyy)|(?:mm\/dd\/yyyy)|(?:yyyy\/mm\/dd)|(?:yyyy\/dd\/mm)|(?:dd[-_]mm[-_]yyyy)|(?:mm[-_]dd[-_]yyyy)|(?:yyyy[-_]mm[-_]dd)|(?:yyyy[-_]dd[-_]mm))$");
                }
                return true;
            })
            .WithMessage("El formato de fecha no es válido.")
            .When(x => x.formatofecha != null);

        RuleFor(x => x.TipoDato).Must((request, tipoDato) =>
        {
            if (tipoDato == TipoDato.entero)
            {
                return request.contenido == null &&
                       request.formatofecha == null &&
                       request.separadorDeDecimales == null &&
                       (request.separadorDeMiles == "," || request.separadorDeMiles == ".");
            }
            else if (tipoDato == TipoDato.conDecimal)
            {
                return request.contenido == null &&
                       request.formatofecha == null &&
                       (request.separadorDeMiles == "," || request.separadorDeMiles == ".") &&
                       (request.separadorDeDecimales == "," || request.separadorDeDecimales == ".") &&
                       request.separadorDeMiles != request.separadorDeDecimales;
            }
            else if (tipoDato == TipoDato.hiperTexto)
            {
                return request.contenido == null &&
                       request.formatofecha == null &&
                       request.separadorDeMiles == null &&
                       request.separadorDeDecimales == null;
            }else if (tipoDato==TipoDato.fecha)
            {
                return request.contenido == null &&
                       request.formatofecha != null &&
                       request.separadorDeMiles == null &&
                       request.separadorDeDecimales == null &&
                       request.Longitud == 0;


            }

            return false;
        }).WithMessage("Las propiedades no cumplen las condiciones requeridas para el TipoDato especificado.");
    }
}
