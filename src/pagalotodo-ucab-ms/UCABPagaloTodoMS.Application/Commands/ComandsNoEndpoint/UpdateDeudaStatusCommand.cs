using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class UpdateDeudaStatusCommand : IRequest<Guid>
    {
        public UpdateDeudaStatusRequest _request { get; set; }

        public UpdateDeudaStatusCommand(UpdateDeudaStatusRequest request)
        {
            _request = request;
        }
    }
}
