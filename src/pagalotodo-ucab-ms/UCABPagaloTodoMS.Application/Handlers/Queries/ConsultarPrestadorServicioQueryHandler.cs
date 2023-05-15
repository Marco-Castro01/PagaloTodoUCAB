using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;

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
                else
                {
                    return HandleAsync();
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarPrestadorServicioQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<List<PrestadorServicioResponse>> HandleAsync()
        {
            try
            {
                _logger.LogInformation("ConsultarPrestadorServicioQueryHandler.HandleAsync");

                var result = _dbContext.PrestadorServicio.Select(c => new PrestadorServicioResponse()
                {
                    Id = c.Id,
                    rif = c.rif,
                    nickName = c.nickName,
                    status = c.status,
                    email = c.email,
                    password = c.password
                });

                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarPrestadorServicioQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }
        }
    }
}