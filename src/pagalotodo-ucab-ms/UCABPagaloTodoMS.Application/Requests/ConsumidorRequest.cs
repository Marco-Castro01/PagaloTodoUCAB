using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class ConsumidorRequest
    {
        public string email { get; set; }

        public string password { get; set; }

        public string? name { get; set; }
        public string? cedula { get; set; }
        public string nickName { get; set; }
        public string? lastName { get; set; }
    }
}
