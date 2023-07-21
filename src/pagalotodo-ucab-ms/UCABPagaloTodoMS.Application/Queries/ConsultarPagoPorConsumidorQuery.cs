using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Responses;

public class ConsultarPagoPorConsumidorQuery : IRequest<List<PagoResponse>>
{
    [ExcludeFromCodeCoverage]
    public Guid IdConsumidor { get; set; }

    public ConsultarPagoPorConsumidorQuery(Guid idConsumidor)
    {
        IdConsumidor = idConsumidor;
    }

}