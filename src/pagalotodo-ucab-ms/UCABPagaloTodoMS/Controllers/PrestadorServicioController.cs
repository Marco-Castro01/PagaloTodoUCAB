using MediatR;
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
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Prestadores de servicios. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }
        
        
        /// <summary>
        ///     Endpoint para la creacion de servicio
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> creacionYAsignacionDeServicio(Guid prestadorServicioId,[FromBody] ServicioRequest ServicioRequests)
        {
            _logger.LogInformation("Entrando al método que envia cambia la contrasena");
            try
            {
                var query = new AsignarServicioComand(prestadorServicioId,ServicioRequests);
                var response = await _mediator.Send(query);
                return Ok(response);
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
        
        
        
        
        
        
        
        [HttpDelete("/prestador/{prestadorId}/enviarArchivo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ArchivoDeConciliacion(IFormFile file, Guid prestadorId)
        {
            _logger.LogInformation("Entrando al método que Elimina");
            try
            { 
                if (file == null && file.Length == 0)
                    return BadRequest("Debe seleccionar un archivo para cargar.");

                var query = new RecibirArchivoConciliacionCommand(file,prestadorId);
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
