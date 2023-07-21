using MediatR;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class GetInfoConsumidorQuery : IRequest<ConsumidorResponse>
    {
        public Guid _idConsumidor { get; set; }
        public GetInfoConsumidorQuery(Guid idConsumidor)
        {
            _idConsumidor = idConsumidor;
        }
    }
}