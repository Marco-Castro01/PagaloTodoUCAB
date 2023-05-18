using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class UsuariosResponse
    {
        public Guid Id { get; set; }
        public string Discriminator { get; set; }
    }
}
