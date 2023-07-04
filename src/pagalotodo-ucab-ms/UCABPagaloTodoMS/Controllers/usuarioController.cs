using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Base;
using UCABPagaloTodoMS.Application.Mailing;

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class usuarioController : BaseController<usuarioController>
    {
        private readonly IMediator _mediator;
        private readonly IEmailSender _emailSender;

        public usuarioController(ILogger<usuarioController> logger, IMediator mediator, IEmailSender emailSender) : base(logger)
        {
            _mediator = mediator;
            _emailSender = emailSender;
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
                string id = User.FindFirstValue("Id");
                if (string.IsNullOrEmpty(id))
                    return StatusCode(422,"Error con Usuario: Debe loguearse"); 
                var query = new ConsultarUsuariosQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        [Authorize(Roles = ("AdminEntity"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> EditarUsuario(EditarUsuarioRequest user)
        {
            _logger.LogInformation("Entrando al método que edita los usuarios");
            try
            {
                string id = User.FindFirstValue("Id");
                //if (string.IsNullOrEmpty(id))
                    //return StatusCode(422,"Error con Usuario: Debe loguearse");
                var query = new EditarUsuarioCommand(user);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la edicion de usuarios: " + ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Agregaradmin(AdminRequest usuario)
        {
            _logger.LogInformation("Entrando al método que registra los usuarios");
            try
            {
                string id = User.FindFirstValue("Id");
                //if (string.IsNullOrEmpty(id))
                    //return StatusCode(422,"Error con Usuario: Debe loguearse");
                var query = new AgregarAdminCommand(usuario);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
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
        [HttpPost("Consumidor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Agregarconsumidor(ConsumidorRequest usuario)
        {
            _logger.LogInformation("Entrando al método que registra los usuarios");
            try
            {
                var query = new AgregarConsumidorCommand(usuario);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
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
        [HttpPost("Prestador")]
        [Authorize(Roles = ("AdminEntity"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Agregarprestador(PrestadorRequest usuario)
        {
            _logger.LogInformation("Entrando al método que registra los usuarios");
            try
            {
                string id = User.FindFirstValue("Id");
                //if (string.IsNullOrEmpty(id))
                    //return StatusCode(422,"Error con Usuario: Debe loguearse");
                var query = new AgregarPrestadorCommand(usuario);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
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
                return BadRequest(ex.Message);
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
            catch (CustomException ex)
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
            catch (CustomException ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un usuario. Exception: " + ex);
                return BadRequest(ex.Message); 
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar cambiar la contrasena. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Password")]
        [Authorize(Roles = "AdminEntity, ConsumidorEntity,PrestadorServicioEntity")]
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
            catch (CustomException ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un usuario. Exception: " + ex);
                return BadRequest(ex.Message); 
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar actualizar la contrasena. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///     Endpoint que registra un valor.
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Post registra valor de prueba.
        ///     ## Url
        ///     POST /CamposConciliacion/CamposConciliacion
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna el id del nuevo registro.</returns>
        [HttpDelete("/usuario/{idUsuario}/delete")]
        [Authorize(Roles = "AdminEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> DeleteUsuario(Guid idUsuario)
        {
            _logger.LogInformation("Entrando al método que registra los CamposConciliacion");
            try
            {
                var query = new DeleteUsuarioCommand(idUsuario);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Codigo,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un admin. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }


    }
}
