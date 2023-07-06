using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class PrestadorRequest
    {
        public string email { get; set; }

        public string password { get; set; }

        public string? name { get; set; }
        public string nickName { get; set; }
        public string? rif { get; set; }
    }
}
