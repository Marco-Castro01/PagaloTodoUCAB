using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.CustomExceptions;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarPrestadorServicioQueryHandler : IRequestHandler<ConsultarPrestadorServicioPruebaQuery, List<PrestadorServicioResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarPrestadorServicioQueryHandler> _logger;

        public ConsultarPrestadorServicioQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarPrestadorServicioQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<PrestadorServicioResponse>> Handle(ConsultarPrestadorServicioPruebaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarPrestadorServicioQueryHandler.Handle: Request nulo.");
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
            catch (Exception ex)
            {
                _logger.LogWarning("ConsultarPrestadorServicioQueryHandler.Handle: ArgumentNullException");
                throw new CustomException(ex.Message);
            }
        }

        private async Task<List<PrestadorServicioResponse>> HandleAsync()
        {
            try
            {
                _logger.LogInformation("ConsultarPrestadorServicioQueryHandler.HandleAsync");

                // Consulta todos los registros de la tabla PrestadorServicio
                var result = _dbContext.PrestadorServicio.Select(c => new PrestadorServicioResponse()
                {
                    Id = c.Id,
                    rif = c.rif,
                    nickName = c.nickName,
                    status = c.status,
                    email = c.email,
                });

                // Ejecuta la consulta y devuelve los resultados como una lista
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarPrestadorServicioQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException(ex.Message);
            }
        }
    }
}
