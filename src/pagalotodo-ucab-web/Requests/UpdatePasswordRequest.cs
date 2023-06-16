using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class UpdatePasswordRequest
    {
        public string email { get; set; }
        public string Password { get; set; }
    }
}
