using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Responses
{
    [ExcludeFromCodeCoverage]
    public class PasswordResetResponse
    {
        public string? info { get; set; }
        public string? email { get; set; }
    }
}
