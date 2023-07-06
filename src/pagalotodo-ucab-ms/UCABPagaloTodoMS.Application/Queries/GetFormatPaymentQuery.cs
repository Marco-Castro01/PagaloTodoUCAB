using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class GetFormatPaymentQuery : IRequest<List<CamposPagosRequest>>
    {
        public Guid _idServicio { get; set; }

        public GetFormatPaymentQuery(Guid idServicio)
        {
            _idServicio = idServicio;
        }
        
    }
}

