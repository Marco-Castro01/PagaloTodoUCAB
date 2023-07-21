using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class EditarUsuarioRequest
    {
        public string name { get; set; }
        public string? cedula { get; set; }
        public string? rif { get; set; }
        public string nickName { get; set; }
        public bool status { get; set; }
    }
}
