using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Responses;

public class ConsultarPagoPorServicioQuery : IRequest<List<PagoResponse>>
{
    [ExcludeFromCodeCoverage]
    public Guid _idPrestador { get; set; }
    public Guid _idServicio { get; set; }

    public ConsultarPagoPorServicioQuery(Guid idPrestador,Guid idServicio)
    {
        _idPrestador = idPrestador;
        _idServicio = idServicio;
    }

}