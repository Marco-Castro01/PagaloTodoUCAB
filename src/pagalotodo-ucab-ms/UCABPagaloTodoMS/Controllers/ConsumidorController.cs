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

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumidorController : BaseController<ConsumidorController>
    {
        private readonly IMediator _mediator;

        public ConsumidorController(ILogger<ConsumidorController> logger, IMediator mediator) : base(logger)
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
        ///     GET /Consumidores/consumidores
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de PrestadoresServicios.</returns>
        [HttpGet("consumidores/consulta")]
        [Authorize(Roles = "AdminEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<ConsumidorResponse>>> ConsultaConsumidores()
        {
            _logger.LogInformation("Entrando al método que consulta los Consumidores");
            try
            {
               
                var query = new ConsultarConsumidorPruebaQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Codigo,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Consumidores. Exception: " + ex);
                return BadRequest("Ocurrio un error en la consulta de los Consumidores. Exception: " + ex);
            }
            
            
            
            
        }
        
        /// <summary>
        ///     Endpoint para la consulta de prueba
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Get admins
        ///     ## Url
        ///     GET /Consumidores/consumidores
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de PrestadoresServicios.</returns>
        [HttpGet("consumidor/info")]
        [Authorize(Roles = "ConsumidorEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<ConsumidorResponse>>> getInfoConsumidor()
        {
            _logger.LogInformation("Entrando al método que consulta los Consumidores");
            try
            {
                string id = User.FindFirstValue("Id");
                if (string.IsNullOrEmpty(id))
                    return StatusCode(422,"Error con Usuario: Debe loguearse");
                Guid idConsumidor = new Guid(id);
                var query = new GetInfoConsumidorQuery(idConsumidor);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Codigo,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Consumidores. Exception: " + ex);
                return BadRequest("Ocurrio un error en la consulta de los Consumidores. Exception: " + ex);
            }
        }
    }
}
