using MediatR;
using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetFormatPaymentQuery : IRequest<List<CamposPagosRequest>>
    {
        public Guid _idServicio { get; set; }

        public GetFormatPaymentQuery(Guid idServicio)
        {
            _idServicio = idServicio;
        }
        
    }
}

