using MediatR;
using Microsoft.AspNetCore.Mvc;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Base;


namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class usuarioController : BaseController<usuarioController>
    {
        private readonly IMediator _mediator;

        public usuarioController(ILogger<usuarioController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }


        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Agregarusuario(UsuarioRequest usuario)
        {
            _logger.LogInformation("Entrando al método que registra los usuarios");
            try
            {
                var query = new AgregarUsuarioCommand(usuario);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un usuario. Exception: " + ex);
                throw;
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuariosResponse>> login(LoginRequest usuario)
        {
            _logger.LogInformation("Entrando al método que logea los usuarios");
            try
            {
                var query = new LoginQuery(usuario);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un usuario. Exception: " + ex);
                throw;
            }
        }


    }
}
