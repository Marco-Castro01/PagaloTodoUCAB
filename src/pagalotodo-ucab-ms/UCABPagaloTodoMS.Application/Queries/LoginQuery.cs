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
    public class LoginQuery : IRequest<UsuariosResponse>
    {
        public LoginRequest _request { get; set; }

        public LoginQuery(LoginRequest request)
        {
            _request = request;
        }
    }
}
