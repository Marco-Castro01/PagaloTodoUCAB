using MediatR;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class ConsultarConsumidorPruebaQuery : IRequest<List<ConsumidorResponse>>
    { }
}