using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class ChangePasswordRequest
    {
        public string token { get; set; }
        public string password { get; set; }

    }
}
