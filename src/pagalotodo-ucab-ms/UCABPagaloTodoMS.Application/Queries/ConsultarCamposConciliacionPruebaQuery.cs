using MediatR;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class ConsultarCamposConciliacionPruebaQuery : IRequest<List<CamposConciliacionResponse>>
    { }
}

