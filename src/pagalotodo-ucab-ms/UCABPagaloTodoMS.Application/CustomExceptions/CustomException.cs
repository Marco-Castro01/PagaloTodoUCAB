using Microsoft.Azure.Amqp.Framing;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace UCABPagaloTodoMS.Application.CustomExceptions
{
    public class CustomException : Exception
    {
        public int Codigo { get; set; }

        public CustomException()
        {
        }

        public CustomException(string message)
            : base(message)
        {
        }
        public CustomException(int codigo, string message)
            : base(message)
        {
            Codigo = codigo;
        }

        public CustomException(string message, Exception inner)
            : base(message, inner)
        {
        }
        
        public CustomException(int codigo,string message, Exception inner)
            : base(message, inner)
        {
        }



        public CustomException(int codigo, string mensaje, Exception innerException, List<Error> errores)
            : base(mensaje, innerException)
        {
            Codigo = codigo;
        }

        public static CustomException CrearDesdeListaException(int codigo, string mess,ValidationException ex)
        {
            string mensaje = $"{mess}: {Environment.NewLine}";

            foreach (var error in ex.Errors.OrderBy(e => e.PropertyName))
            {
                mensaje += $"- {error.PropertyName}: {error.ErrorMessage}{Environment.NewLine}";
            }

            return new CustomException(codigo, mensaje, ex, ex.Errors.Select(e => new Error { Campo = e.PropertyName, Mensaje = e.ErrorMessage }).ToList());
        }

/*
        public CustomException FormatListaDeExcepciones(List<ValidationFailure> validationFailures)
        {
            string errores =String.Join("\n",validationFailures.Select(e => e.ErrorMessage).ToList());

            return new CustomException()
        }
        */
    }

    public class Error
    {
        public string Campo { get; set; }
        public string Mensaje { get; set; }
    }
}