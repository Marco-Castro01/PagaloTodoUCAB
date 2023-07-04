using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Mailing;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Base;

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrestadorServicioController : BaseController<PrestadorServicioController>
    {
        private readonly IMediator _mediator;

        public PrestadorServicioController(ILogger<PrestadorServicioController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Endpoint para la consulta de prueba
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Get admins
        ///     ## Url
        ///     GET /PrestadoresServicios/PrestadoresServicios
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de PrestadoresServicios.</returns>
        [HttpGet("prestadores_servicios")]
        [Authorize(Roles = "AdminEntity, ConsumidorEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PrestadorServicioResponse>>> ConsultaPrestadorServicio()
        {
            _logger.LogInformation("Entrando al método que consulta los Prestadores de Servicios");
            try
            {
                var query = new ConsultarPrestadorServicioPruebaQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, "Error al consultar los Prestadores de servicios");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la consulta de los Prestadores de servicios");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Endpoint para la creacion y asignacion de servicio
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Post Servicios
        ///     ## Url
        ///     POST /
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de valores ejemplo.</returns>
        [HttpPost("/prestador/{prestadorServicioId}/servicio")]
        [Authorize(Roles = "AdminEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> CreacionYAsignacionDeServicio(Guid prestadorServicioId, [FromBody] ServicioRequest servicioRequest)
        {
            _logger.LogInformation("Entrando al método para la creación y asignación de servicio");
            try
            {
                var command = new AsignarServicioComand(prestadorServicioId, servicioRequest);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, "Error al intentar registrar un usuario");
                return StatusCode(422, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar cambiar la contraseña");
                return BadRequest(ex);
            }
        }
        /// <summary>
        ///     Endpoint para El envio del archivo conciliadoo
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Post Servicios
        ///     ## Url
        ///     POST /
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de valores ejemplo.</returns>
        [HttpPost("/prestador/Archivo_conciliacion/enviar_Archivo")]
        [Authorize(Roles = "PrestadorServicioEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ArchivoDeConciliacion(IFormFile file)
        {
            _logger.LogInformation("Entrando al método que envia el archivo de conciliacion de respuesta");
            try
            {
                string id = User.FindFirstValue("Id");
                if (string.IsNullOrEmpty(id))
                    return StatusCode(422,"Error con Usuario: Debe loguearse");
                if (file == null || file.Length == 0)
                    return BadRequest("Debe seleccionar un archivo para cargar.");
                Guid prestadorId = new Guid(id);

                var command = new RecibirArchivoConciliacionCommand(file, prestadorId);
                var response = await _mediator.Send(command);
                return Ok("Envio Exitoso");
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Codigo,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar enviar archivo");
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        ///     Endpoint para El envio del archivo conciliadoo
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Post Servicios
        ///     ## Url
        ///     POST /
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de valores ejemplo.</returns>
        [HttpPost("/prestador/archivoVerificacion/{servicioId}/enviarArchivo")]
        [Authorize(Roles = "PrestadorServicioEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ArchivoDeVerificacion(IFormFile file, Guid servicioId)
        {
            _logger.LogInformation("Entrando al método que envia el archivo de conciliacion de respuesta");
            try
            {
                string id = User.FindFirstValue("Id");
                if (string.IsNullOrEmpty(id))
                    return StatusCode(422,"Error con Usuario: Debe loguearse");
                if (file == null || file.Length == 0)
                    return BadRequest("Debe seleccionar un archivo para cargar.");
                Guid prestadorId = new Guid(id);
                var command = new RecibirArchivoVerificacionCommand(file, servicioId,prestadorId);
                var response = await _mediator.Send(command);
                return Ok("Envio Exitoso");
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Codigo,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar enviar archivo");
                return BadRequest(ex.Message);
            }
        }
        
        
        
        /// <summary>
        ///     Endpoint para la configuracion de los campos a recibir en los pagos
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Post Servicios
        ///     ## Url
        ///     POST /
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna mensaje de confirmarcion.</returns>

        [HttpPatch("servicio/{servicioId}/formatoPago")]
        [Authorize(Roles = "PrestadorServicioEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ConfFormtatoPago(Guid servicioId, List<CamposPagosRequest> listaCampos)
        {
            _logger.LogInformation("Entrando al método que asigna el formato de pago");
            try
            {
                string id = User.FindFirstValue("Id");
                if (string.IsNullOrEmpty(id))
                    return StatusCode(422,"Error con Usuario: Debe loguearse");
                Guid prestadorId = new Guid(id);
                var command = new ConfFormatoPagoCommand(prestadorId,servicioId, listaCampos);
                var response = await _mediator.Send(command);
                return Ok("Asignación exitosa");
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, "Error al intentar asignar el formato de pago");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar asignar el formato de pago");
                return BadRequest(ex.Message);
            }
        }
    }
}
