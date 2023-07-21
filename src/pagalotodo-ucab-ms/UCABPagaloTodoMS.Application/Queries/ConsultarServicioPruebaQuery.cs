﻿using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    [ExcludeFromCodeCoverage]
    public class ConsultarServicioPruebaQuery : IRequest<List<ServicioResponse>>
    { }
}

