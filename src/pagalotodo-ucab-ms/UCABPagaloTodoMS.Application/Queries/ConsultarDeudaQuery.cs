using MediatR;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

public class ConsultarDeudaQuery : IRequest<List<DeudaResponse>>
{
    public Guid IdDeuda { get; set; }

    public ConsultarDeudaQuery(Guid idDeuda)
    {
        IdDeuda= idDeuda;
    }

}