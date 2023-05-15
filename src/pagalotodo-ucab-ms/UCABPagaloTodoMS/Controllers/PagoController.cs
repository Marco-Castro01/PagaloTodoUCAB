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
    public class PagoController : BaseController<PagoController>
    {
        private readonly IMediator _mediator;

        public PagoController(ILogger<PagoController> logger, IMediator mediator) : base(logger)
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
        ///     GET /Pagos/Pagos
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de Pagos.</returns>
        [HttpGet("pagos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PagoResponse>>> ConsultaPago()
        {
            _logger.LogInformation("Entrando al método que consulta los Pagos");
            try
            {
                var query = new ConsultarPagoPruebaQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Pagos. Exception: " + ex);
                throw;
            }
        }

        /// <summary>
        ///     Endpoint que registra un Pago.
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Post registra Pagos
        ///     ## Url
        ///     POST /PrestadorServicio/PrestadorServicio
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna el id del nuevo registro.</returns>
        [HttpPost("pago")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AgregarPago(PagoRequest Pago)
        {
            _logger.LogInformation("Entrando al método que registra los Admins");
            try
            {
                var query = new AgregarPagoPruebaCommand(Pago);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un pago. Exception: " + ex);
                throw;
            }
        }
    }
}
