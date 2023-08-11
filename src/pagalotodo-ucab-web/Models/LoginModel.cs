using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoWeb.Requests;

namespace UCABPagaloTodoWeb.Models
{
	public class LoginModel
	{
            public LoginRequest? Login { get; set; }
            public ConsumidorRequest? Consumidor { get; set; }
    }
}
