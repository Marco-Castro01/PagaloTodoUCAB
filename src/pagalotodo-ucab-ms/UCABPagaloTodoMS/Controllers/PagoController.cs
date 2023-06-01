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
        ///     Endpoint para la consulta de pagoDirecto
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
                return BadRequest("Error en la consulta");
            }
        }

        //-------------------------------------------------------
        /// <summary>
        ///     Endpoint para la consulta de pagoDirecto
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Get admins
        ///     ## Url
        ///     GET /Pagos/PagosPorConsumidor
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de Pagos.</returns>
        [HttpGet("pagosPorConsumidor/{IdConsumidor}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PagoResponse>>> ListarPagosPorIdConsumidor(Guid IdConsumidor)
        {
            _logger.LogInformation("Entrando al método que Lista los pagos realizados por El consumidor");
            try
            {
                var query = new ConsultarPagoPorConsumidorQuery(IdConsumidor);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Pagos. Exception: " + ex);
                return BadRequest("Error en la consulta");
            }
        }

        //-----------------------------------------------------------------------
        /// <summary>
        ///     Endpoint para la consulta de pagoDirecto
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Get admins
        ///     ## Url
        ///     GET /Pagos/PagosPorServicio
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de Pagos.</returns>
        [HttpGet("pagosPorServicio/{IdServicio}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PagoResponse>>> ListarPagosPorIdPrestador(Guid IdServicio)
        {
            _logger.LogInformation("Entrando al método que Lista los pagos realizados por El consumidor");
            try
            {
                var query = new ConsultarPagoPorServicioQuery(IdServicio);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Pagos. Exception: " + ex);
                return BadRequest("Error en la consulta");
            }
        }

        //-----------------------------------------------------------------------

       
       
        
        /// <summary>
        ///     Endpoint que registra un pagoDirecto.
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Post registra Pagos
        ///     ## Url
        ///     POST /pago/pagoDirecto
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna mensaje de confirmacion o de error.</returns>
        [HttpPost("pagoDirecto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AgregarPagoDirecto(PagoDirectoRequest pagoDirecto)
        {
            _logger.LogInformation("Entrando al método que registra los Admins");
            try
            {
                var query = new AgregarPagoDirectoCommand(pagoDirecto);
                var response = await _mediator.Send(query);
                return Ok("Pago Exitoso");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un pago. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }
        
        
        
        
        
        //-----------------------------------------------------------------------

       
    
        /// <summary>
        ///     Endpoint que registra un pagoDirecto.
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Post registra Pagos
        ///     ## Url
        ///     POST /pago/pagoPorValidacion
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna mensaje de confirmacion o de error.</returns>
        [HttpPost("pagoPorValidacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AgregarPagoPorValidacion(PagoPorValidacionRequest pagoPorValidacion)
        {
            _logger.LogInformation("Entrando al método que registra los Admins");
            try
            {
                var query = new AgregarPagoPorValidacionCommand(pagoPorValidacion);
                var response = await _mediator.Send(query);
                return Ok("Pago exitoso");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un pago. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
