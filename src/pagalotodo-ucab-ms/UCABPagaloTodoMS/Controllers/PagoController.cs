using System.IdentityModel.Tokens.Jwt;
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
        [Authorize(Roles = "AdminEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PagoResponse>>> ConsultaPago()
        {
            _logger.LogInformation("Entrando al método que consulta los Pagos");
            try
            {
                string id = User.FindFirstValue("Id");
                //if (string.IsNullOrEmpty(id))
                    //return StatusCode(422,"Error con Usuario: Debe loguearse");
                var query = new ConsultarPagoPruebaQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Codigo,ex.Message);
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
        [HttpGet("pagosPorConsumidor/pagosHechos")]
        [Authorize(Roles = "ConsumidorEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PagoResponse>>> ListarPagosPorIdConsumidor()
        {
            _logger.LogInformation("Entrando al método que Lista los pagos realizados por El consumidor");
            try
            {
                string id = User.FindFirstValue("Id");
                if (string.IsNullOrEmpty(id))
                    return StatusCode(422,"Error con Usuario: Debe loguearse");
                Guid IdConsumidor = new Guid(id);
                var query = new ConsultarPagoPorConsumidorQuery(IdConsumidor);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Codigo,ex.Message);
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
        [HttpGet("pagosPorServicio/pagos_recibidos")]
        [Authorize(Roles = "PrestadorServicioEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PagoResponse>>> ListarPagosPorIdServicior(Guid IdServicio)
        {
            _logger.LogInformation("Entrando al método que Lista los pagos realizados por El consumidor");
            try
            {
                string id = User.FindFirstValue("Id");
                if (string.IsNullOrEmpty(id))
                    return StatusCode(422,"Error con Usuario: Debe loguearse");
                Guid idPrestador = new Guid(id);
                var query = new ConsultarPagoPorServicioQuery(idPrestador,IdServicio);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Codigo,ex.Message);
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
        [HttpPost("servicio/{idServicio}/pagoDirecto")]
        [Authorize(Roles = "ConsumidorEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AgregarPagoDirecto(PagoDirectoRequest pagoDirecto,Guid idServicio)
        {
            _logger.LogInformation("Entrando al método que registra El pago");
            try
            {
                string id = User.FindFirstValue("Id");
                if (string.IsNullOrEmpty(id))
                    return StatusCode(422,"Error con consumidor: Debe loguearse");
                Guid idConsumidor = new Guid(id);

                var query = new AgregarPagoDirectoCommand(pagoDirecto,idServicio,idConsumidor);
                var response = await _mediator.Send(query);


                return Ok("Pago Exitoso");
            } 
            catch (CustomException ex)
            {
                return StatusCode(ex.Codigo,ex.Message);
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
        [HttpPost("/Consumidor/pago/{idDeuda}/pagar")]
        [Authorize(Roles = "ConsumidorEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AgregarPagoPorValidacion(PagoVerificacionRequest pagoVerif,Guid idDeuda)
        {
            _logger.LogInformation("Entrando al método que registra los Admins");
            try
            {
                string id =User.FindFirstValue("Id");
                if (string.IsNullOrEmpty(id))
                    return StatusCode(422,"Error con Usuario: Debe loguearse");
                var idConsumidor= new Guid(id);
                var query = new AgregarPagoPorverificacionCommand(pagoVerif, idConsumidor,idDeuda);
                var response = await _mediator.Send(query);
                return Ok("Pago exitoso");
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Codigo,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un pago. Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
