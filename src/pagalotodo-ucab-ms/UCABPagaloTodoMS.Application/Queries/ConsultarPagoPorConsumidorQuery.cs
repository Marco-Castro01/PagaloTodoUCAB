using MediatR;
using UCABPagaloTodoMS.Application.Responses;

public class ConsultarPagoPorConsumidorQuery : IRequest<List<PagoResponse>>
{
    public Guid IdConsumidor { get; set; }

    public ConsultarPagoPorConsumidorQuery(Guid idConsumidor)
    {
        IdConsumidor = idConsumidor;
    }

}