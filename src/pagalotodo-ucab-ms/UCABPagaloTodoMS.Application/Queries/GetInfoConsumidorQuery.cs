using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetInfoConsumidorQuery : IRequest<ConsumidorResponse>
    {
        public Guid _idConsumidor { get; set; }
        public GetInfoConsumidorQuery(Guid idConsumidor)
        {
            _idConsumidor = idConsumidor;
        }
    }
}