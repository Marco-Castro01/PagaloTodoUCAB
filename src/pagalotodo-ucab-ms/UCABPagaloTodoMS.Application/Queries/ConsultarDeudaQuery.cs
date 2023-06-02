using MediatR;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

public class ConsultarDeudaQuery : IRequest<List<DeudaResponse>>
{
    public string Identificador { get; set; }

    public ConsultarDeudaQuery(string identificador)
    {
        Identificador= identificador;
    }

}