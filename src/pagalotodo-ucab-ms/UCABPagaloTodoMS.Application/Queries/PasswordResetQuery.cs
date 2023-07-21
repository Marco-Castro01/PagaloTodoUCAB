using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class PasswordResetQuery : IRequest<PasswordResetResponse>
    {
        public ResetPasswordRequest _request { get; set; }

        public PasswordResetQuery(ResetPasswordRequest request)
        {
            _request = request;
        }
    }
}
