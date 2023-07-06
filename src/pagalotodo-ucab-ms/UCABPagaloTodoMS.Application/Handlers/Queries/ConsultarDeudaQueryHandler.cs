using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarDeudaQueryHandler : IRequestHandler<ConsultarDeudaQuery, List<DeudaResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarDeudaQueryHandler> _logger;
        private readonly IMediator _mediator;

        public ConsultarDeudaQueryHandler(IUCABPagaloTodoDbContext dbContext, IMediator mediator,ILogger<ConsultarDeudaQueryHandler> logger)
        {
            _mediator = mediator;
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<DeudaResponse>> Handle(ConsultarDeudaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarDeudaQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return HandleAsync(request);
            }
            catch (ArgumentNullException ex)
            {
                throw new CustomException("RequestNulo", ex);
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("ConsultarDeudaQueryHandler.Handle: ArgumentNullException");
                throw new CustomException(ex.Message);
            }
        }

        private async Task<List<DeudaResponse>> HandleAsync(ConsultarDeudaQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarPagoQueryHandler.HandleAsync");
                var servicio = _dbContext.Servicio.FirstOrDefault(o=>o.Id==request._idServicio);
                // Consulta los registros de la tabla Deuda que coincidan con el identificador y deudaStatus especificados en la consulta
                var result = await _dbContext.Deuda
                    .Where(c => c.identificador == request._request.identificador && c.deudaStatus == false && c.deleted==false)
                    .Select(c => new DeudaResponse()
                    {
                        idDeuda = c.Id,
                        identificador = request._request.identificador,
                        servicioId = c.servicio.Id,
                        servicioName = c.servicio.name,
                        deuda = c.deuda,
                        camposPagos=  JsonConvert.DeserializeObject<List<CamposPagosRequest>>(servicio.formatoDePagos),
                    }).ToListAsync();
                

                // Ejecuta la consulta y devuelve los resultados como una lista
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarPagoQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException("Error en consulta", ex);
            }
        }

    
        

    }
}
