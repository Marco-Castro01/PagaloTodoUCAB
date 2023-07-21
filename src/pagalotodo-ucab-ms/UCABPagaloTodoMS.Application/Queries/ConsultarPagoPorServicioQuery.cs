using MediatR;
using UCABPagaloTodoMS.Application.Responses;

public class ConsultarPagoPorServicioQuery : IRequest<List<PagoResponse>>
{
    public Guid _idPrestador { get; set; }
    public Guid _idServicio { get; set; }

    public ConsultarPagoPorServicioQuery(Guid idPrestador,Guid idServicio)
    {
        _idPrestador = idPrestador;
        _idServicio = idServicio;
    }

}