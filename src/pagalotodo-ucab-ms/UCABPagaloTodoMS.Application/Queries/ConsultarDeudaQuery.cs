using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

public class ConsultarDeudaQuery : IRequest<List<DeudaResponse>>
{
    public Guid _idServicio { get; set; }
    public  GetDeudaRequest _request { get; set; }

    public ConsultarDeudaQuery(GetDeudaRequest request,Guid idServicio)
    {
        _request= request;
        _idServicio = idServicio;
    }

}