﻿using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    [ExcludeFromCodeCoverage]
    public class ConsultarConsumidorPruebaQuery : IRequest<List<ConsumidorResponse>>
    { }
}