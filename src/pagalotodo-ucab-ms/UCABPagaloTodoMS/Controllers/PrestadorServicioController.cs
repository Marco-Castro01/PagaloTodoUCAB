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
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Prestadores de servicios. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
