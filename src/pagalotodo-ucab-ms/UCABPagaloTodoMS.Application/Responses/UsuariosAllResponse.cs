using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class UsuariosAllResponse
    {
        public Guid Id { get; set; }
        public string? email { get; set; }
        public string? name { get; set; }
        public string? cedula { get; set; }
        public string nickName { get; set; }
        public bool status { get; set; }
        public string? Discriminator;
    }
}
