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
    public class CamposConciliacionController : BaseController<CamposConciliacionController>
    {
        private readonly IMediator _mediator;

        public CamposConciliacionController(ILogger<CamposConciliacionController> logger, IMediator mediator) : base(logger)
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
        ///     GET /CamposConciliacion/CamposConciliacion
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna la lista de CamposConciliacion.</returns>
        [HttpGet("CamposConciliacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CamposConciliacionResponse>>> ConsultaCamposConciliacion()
        {
            _logger.LogInformation("Entrando al método que consulta los CamposConciliacion");
            try
            {
                var query = new ConsultarCamposConciliacionPruebaQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los CamposConciliacion. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Endpoint que registra un valor.
        /// </summary>
        /// <remarks>
        ///     ## Description
        ///     ### Post registra valor de prueba.
        ///     ## Url
        ///     POST /CamposConciliacion/CamposConciliacion
        /// </remarks>
        /// <response code="200">
        ///     Accepted:
        ///     - Operation successful.
        /// </response>
        /// <returns>Retorna el id del nuevo registro.</returns>
        [HttpPost("CamposConciliacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AgregarCamposConciliacion(CamposConciliacionRequest campo)
        {
            _logger.LogInformation("Entrando al método que registra los CamposConciliacion");
            try
            {
                var query = new AgregarCamposConciliacionPruebaCommand(campo);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un admin. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
