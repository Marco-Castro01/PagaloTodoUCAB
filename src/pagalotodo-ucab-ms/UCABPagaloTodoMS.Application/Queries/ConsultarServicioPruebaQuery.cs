using MediatR;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class ConsultarServicioPruebaQuery : IRequest<List<ServicioResponse>>
    { }
}

