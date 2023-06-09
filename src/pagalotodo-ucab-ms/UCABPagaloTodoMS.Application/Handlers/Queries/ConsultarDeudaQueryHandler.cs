using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarDeudaQueryHandler : IRequestHandler<ConsultarDeudaQuery, List<DeudaResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarDeudaQueryHandler> _logger;

        public ConsultarDeudaQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarDeudaQueryHandler> logger)
        {
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

                // Consulta los registros de la tabla Deuda que coincidan con el identificador y deudaStatus especificados en la consulta
                var result = _dbContext.Deuda.Where(c => c.identificador == request.Identificador && c.deudaStatus == false)
                    .Select(c => new DeudaResponse()
                    {
                        idDeuda = c.Id,
                        identificador = request.Identificador,
                        servicioId = c.servicio.Id,
                        servicioName = c.servicio.name,
                        deuda = c.deuda
                    });

                // Ejecuta la consulta y devuelve los resultados como una lista
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarPagoQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException("Error en consulta", ex);
            }
        }
    }
}
