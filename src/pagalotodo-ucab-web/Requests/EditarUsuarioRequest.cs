using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class EditarUsuarioRequest
    {
        public string email { get; set; }
        public string name { get; set; }
        public string cedula { get; set; }
        public string nickName { get; set; }
        public bool status { get; set; }
    }
}
