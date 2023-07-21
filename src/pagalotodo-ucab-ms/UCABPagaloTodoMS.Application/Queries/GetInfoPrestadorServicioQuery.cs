using MediatR;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class GetInfoPrestadorServicioQuery : IRequest<PrestadorServicioResponse>
    {
        public Guid _idPrestador { get; set; }
        public GetInfoPrestadorServicioQuery(Guid idPrestador)
        {
            _idPrestador = idPrestador;
        }
    }
}