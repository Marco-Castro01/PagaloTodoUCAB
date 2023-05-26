using MediatR;
using UCABPagaloTodoMS.Application.Responses;

public class ConsultarPagoPorServicioQuery : IRequest<List<PagoResponse>>
{
    public Guid IdPrestador { get; set; }

    public ConsultarPagoPorServicioQuery(Guid idPrestador)
    {
        IdPrestador = idPrestador;
    }

}