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
            _logger.LogInformation("Entrando al método que consulta los admins");
            try
            {
                var query = new ConsultarPrestadorServicioPruebaQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Prestadores de servicios. Exception: " + ex);
                throw;
            }
        }

        /// <summary>
        ///     Endpoint que registra un valor.
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Post registra valor de prueba.
        ///     ## Url
        ///     POST /PrestadorServicio/PrestadorServicio
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna el id del nuevo registro.</returns>
        [HttpPost("PrestadorServicio")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AgregarPrestadorServicio(PrestadorServicioRequest prestador)
        {
            _logger.LogInformation("Entrando al método que registra los Admins");
            try
            {
                var query = new AgregarPrestadorServicioPruebaCommand(prestador);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un admin. Exception: " + ex);
                throw;
            }
        }
    }
}
