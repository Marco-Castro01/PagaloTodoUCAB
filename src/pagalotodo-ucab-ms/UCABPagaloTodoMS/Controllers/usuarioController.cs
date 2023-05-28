using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
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

        [HttpGet()]
        [Authorize(Roles = ("AdminEntity"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UsuariosAllResponse>>> ConsultaUsuarios()
        {
            _logger.LogInformation("Entrando al método que consulta los usuarios");
            try
            {
                var query = new ConsultarUsuariosQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> EditarUsuario(EditarUsuarioRequest user)
        {
            _logger.LogInformation("Entrando al método que edita los usuarios");
            try
            {
                var query = new EditarUsuarioCommand(user);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la edicion de usuarios: " + ex);
                throw;
            }
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
            catch (UserRegistException ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un usuario. Exception: " + ex);

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un usuario. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<String>> login(LoginRequest usuario)
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


        [HttpPost("ResetToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PasswordResetResponse>> reset(ResetPasswordRequest usuario)
        {
            _logger.LogInformation("Entrando al método que envia token de reset");
            try
            {
                var query = new PasswordResetQuery(usuario);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un usuario. Exception: " + ex);
                throw;
            }
        }



        [HttpPost("Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> changePassword(ChangePasswordRequest usuario)
        {
            _logger.LogInformation("Entrando al método que envia cambia la contrasena");
            try
            {
                var query = new CambiarContrasenaCommand(usuario);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar cambiar la contrasena. Exception: " + ex);
                throw;
            }
        }

        [HttpPut("Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> updatepassword(UpdatePasswordRequest usuario)
        {
            _logger.LogInformation("Entrando al método que actualiza la contrasena");
            try
            {
                var query = new ActualizarContrasenaCommand(usuario);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar actualizar la contrasena. Exception: " + ex);
                throw;
            }
        }


    }
}
