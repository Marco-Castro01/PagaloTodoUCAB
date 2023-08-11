using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.CustomExceptions;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarCamposConciliacionQueryHandler : IRequestHandler<ConsultarCamposConciliacionPruebaQuery, List<CamposConciliacionResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarCamposConciliacionQueryHandler> _logger;

        public ConsultarCamposConciliacionQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarCamposConciliacionQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<CamposConciliacionResponse>> Handle(ConsultarCamposConciliacionPruebaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarCamposConciliacionQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return HandleAsync();
            }
            catch (ArgumentNullException ex)
            {
                throw new CustomException("Request Nulo", ex);
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarCamposConciliacionQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<List<CamposConciliacionResponse>> HandleAsync()
        {
            try
            {
                _logger.LogInformation("ConsultarCamposConciliacionQueryHandler.HandleAsync");

                // Consulta los registros de la tabla CamposConciliacion y los mapea a objetos CamposConciliacionResponse
                var result = _dbContext.CamposConciliacion
                    .Where(x=>x.deleted==false)
                    .Select(c => new CamposConciliacionResponse()
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Longitud = c.Longitud
                });

                // Ejecuta la consulta y devuelve los resultados como una lista
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarCamposConciliacionQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException("Error en consulta",ex);
            }
        }
    }
}
