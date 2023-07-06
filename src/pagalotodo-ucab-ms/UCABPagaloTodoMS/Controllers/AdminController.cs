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
        [HttpGet("admins/lista")]
        [Authorize(Roles = "AdminEntity")]
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
        
        
        
        
        
        
        
        [HttpGet("/admin/prestador_servicio/{idPrestadorServicio}/cierreContable")]
        [Authorize(Roles = "AdminEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CierreContableResponse>>> cierreContable(Guid idPrestadorServicio)
        {
            _logger.LogInformation("Entrando al método que genera el archivo de conciliacion");
            try
            {
                var query = new CierreContableCommand(idPrestadorServicio);
                var response = await _mediator.Send(query);
               
                return Ok(response);
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
        
        
        [HttpGet("/admin/info")]
        [Authorize(Roles = "AdminEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CierreContableResponse>>> getInfoAdmin()
        {
            _logger.LogInformation("Entrando al método que genera el archivo de conciliacion");
            try
            {
                string id = User.FindFirstValue("Id");
                if (string.IsNullOrEmpty(id))
                    return StatusCode(422,"Error con Usuario: Debe loguearse");
                Guid idAdmin = new Guid(id);
                var query = new GetInfoAdminQuery(idAdmin);
                var response = await _mediator.Send(query);
               
                return Ok(response);
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
