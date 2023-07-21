using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetInfoPrestadorServicioQuery : IRequest<PrestadorServicioResponse>
    {
        public Guid _idPrestador { get; set; }
        public GetInfoPrestadorServicioQuery(Guid idPrestador)
        {
            _idPrestador = idPrestador;
        }
    }
}