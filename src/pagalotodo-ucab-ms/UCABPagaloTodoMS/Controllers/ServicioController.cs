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
    public class ServicioController : BaseController<ServicioController>
    {
        private readonly IMediator _mediator;

        public ServicioController(ILogger<ServicioController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Endpoint para la consulta de Pago
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
        [HttpGet("servicio")]
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
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Servicio. Exception: " + ex);
                throw;
            }
        }

        /// <summary>
        ///     Endpoint que registra un Servicio.
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Post registra Servicio
        ///     ## Url
        ///     POST /Servicio/Servicio
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna el id del nuevo registro.</returns>
        [HttpPost("servicio")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AgregarServicio(ServicioRequest Servicio)
        {
            _logger.LogInformation("Entrando al método que registra los Admins");
            try
            {
                var query = new AgregarServicioPruebaCommand(Servicio);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un Servicio. Exception: " + ex);
                throw;
            }
        }
    }
}
