using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Base;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioController : BaseController<ServicioController>
    {
        private readonly IMediator _mediator;

        public ServicioController(ILogger<ServicioController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Endpoint para la consulta de pagoDirecto
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Get admins
        ///     ## Url
        ///     GET /servicio/servicios
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de Servicios.</returns>
        [HttpGet("/servicios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PagoResponse>>> ConsultaServicio()
        {
            _logger.LogInformation("Entrando al método que consulta los Servicios");
            try
            {
                
                var query = new ConsultarServicioPruebaQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Servicio. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        ///     Endpoint que Actualiza un Servicio.
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Patch actualiza Servicio
        ///     ## Url
        ///     PATCH /Servicio/Servicio
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna Mensaje de aceptado.</returns>
        [HttpPatch("/servicio/{idServicio}/update")]
        [Authorize(Roles = "AdminEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> ModificarServicio(UpdateServicioRequest servicio, Guid idServicio)
        {
            _logger.LogInformation("Entrando al método que registra los Admins");
            try
            {
                var query = new UpdateServicioPruebaCommand(idServicio,servicio);
                var response = await _mediator.Send(query);
                return Ok("Actualizacion Realizada Exitosamente");
            }
            catch (CustomException ex)
            {
                _logger.LogError("Ocurrio un error al intentar Modificar un Servicio. Exception: " + ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar Modificar un Servicio. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }
        
        
        
        /// <summary>
        ///     Endpoint para la asignacion de un servicio
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Post Servicios/{servicioId}/Campo
        ///     ## Url
        ///     POST /
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de CamposConciliacionResponse.</returns>
        [HttpPost("/servicio/{servicioId}/campo")]
        [Authorize(Roles = "AdminEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> CreacionYAsignacionDeCampos(Guid servicioId,[FromBody] CamposAsigRequest servicioRequests)
        {
            _logger.LogInformation("Entrando al método que envia cambia la contrasena");
            try
            {
                var query = new AsignarCamposCommand(servicioId,servicioRequests);
                var response = await _mediator.Send(query);

                string mess = "Asignacion exitosa";
                if (response == null)
                    mess = "No se envio campo para asignar";
                return Ok(mess);
                
            }
            catch (CustomException ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un usuario. Exception: " + ex);
                return StatusCode(422,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar cambiar la contrasena. Exception: " + ex);
                return BadRequest(ex);
            }
        }
        
        /// <summary>
        ///     Endpoint que Elimina un Servicio.
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Delete elimina Servicio
        ///     ## Url
        ///     Delete /Servicio/Servicio
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna Mensaje de aceptado.</returns>
        [HttpDelete("servicio/{idServicio}/delete")]
        [Authorize(Roles = "AdminEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> EliminarServicio(Guid idServicio)
        {
            _logger.LogInformation("Entrando al método que Elimina");
            try
            {
                var query = new DeleteServicioPruebaCommand(idServicio);
                var response = await _mediator.Send(query);
                return Ok("Eliminacion Exitosa");
            }
            catch (CustomException ex)
            {
                _logger.LogError("Ocurrio un error al intentar Eliminar un Servicio. Exception: " + ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar Eliminar un Servicio. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
