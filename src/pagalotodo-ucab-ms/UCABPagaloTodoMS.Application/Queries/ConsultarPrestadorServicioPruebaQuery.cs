using MediatR;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class ConsultarPrestadorServicioPruebaQuery : IRequest<List<PrestadorServicioResponse>>
    { }
}

