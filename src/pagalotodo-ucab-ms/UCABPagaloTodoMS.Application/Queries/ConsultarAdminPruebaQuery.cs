using MediatR;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class ConsultarAdminPruebaQuery : IRequest<List<AdminResponse>>
    { }
}

