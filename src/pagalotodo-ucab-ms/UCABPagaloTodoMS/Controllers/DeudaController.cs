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
    public class DeudaController : BaseController<DeudaController>
    {
        private readonly IMediator _mediator;

        public DeudaController(ILogger<DeudaController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        //-------------------------------------------------------
        /// <summary>
        ///     Endpoint para la consulta de pagoDirecto
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Get admins
        ///     ## Url
        ///     GET /Dudas/DudasPorIdentificador
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de Deudas.</returns>
        [HttpGet("deudaPorIdentificador/{identificador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PagoResponse>>> ListarDeudas(string identificador)
        {
            _logger.LogInformation("Entrando al método que Lista los pagos realizados por El consumidor");
            try
            {
                var query = new ConsultarDeudaQuery(identificador);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los Pagos. Exception: " + ex);
                return BadRequest("Error en la consulta");
            }
        }

    }
}
