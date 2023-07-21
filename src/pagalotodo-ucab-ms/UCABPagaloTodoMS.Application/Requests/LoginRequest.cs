using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class LoginRequest
    {
        public string email { get; set; }

        public string Password { get; set; }

    }
}
