using MediatR;
using UCABPagaloTodoMS.Application.Responses;

public class ConsultarPagoPorPrestadorQuery : IRequest<List<PagoResponse>>
{
    public Guid IdPrestador { get; set; }

    public ConsultarPagoPorPrestadorQuery(Guid idPrestador)
    {
        IdPrestador = idPrestador;
    }

}