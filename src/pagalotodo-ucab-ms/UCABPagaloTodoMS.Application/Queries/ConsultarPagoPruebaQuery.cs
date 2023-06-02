using MediatR;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class ConsultarPagoPruebaQuery : IRequest<List<PagoResponse>>
    { }
}

