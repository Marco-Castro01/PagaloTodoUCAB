using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public class AdminController : BaseController<AdminController>
    {
        private readonly IMediator _mediator;

        public AdminController(ILogger<AdminController> logger, IMediator mediator) : base(logger)
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
        ///     GET /admins/admins
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de valores ejemplo.</returns>
        [HttpGet("admins")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AdminResponse>>> ConsultaAdmins()
        {
            _logger.LogInformation("Entrando al método que consulta los admins");
            try
            {
                var query = new ConsultarAdminPruebaQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Admins. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }
        
        
        
        
        
        
        
        [HttpGet("prestador_servicio/{idPrestadorServicio}/crear_archivo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> generacionArchivo(Guid idPrestadorServicio)
        {
            _logger.LogInformation("Entrando al método que genera el archivo de conciliacion");
            try
            {
                var query = new CrearYEnviarArchivoConciliacionCommand(idPrestadorServicio);
                var response = await _mediator.Send(query);
                if(response.Equals("No posee servicios asociados"))
                    return StatusCode(204,response);
                return response;
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Codigo,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Admins. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }


    }
}
