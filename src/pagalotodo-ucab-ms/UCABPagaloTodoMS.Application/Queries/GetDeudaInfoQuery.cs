using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

public class GetDeudaInfoQuery : IRequest<DeudaResponse>
{
    public Guid _idDeuda { get; set; }

    public GetDeudaInfoQuery(Guid idDeuda)
    {

        _idDeuda = idDeuda;
    }

}