using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.CustomExceptions;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarAdminQueryHandler : IRequestHandler<ConsultarAdminPruebaQuery, List<AdminResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarAdminQueryHandler> _logger;

        public ConsultarAdminQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarAdminQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<AdminResponse>> Handle(ConsultarAdminPruebaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarAdminQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return HandleAsync();
            }
            catch (ArgumentException ex)
            {
                throw new CustomException("Request nulo", ex);
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarAdminQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<List<AdminResponse>> HandleAsync()
        {
            try
            {
                _logger.LogInformation("ConsultarAdminQueryHandler.HandleAsync");

                // Consulta los registros de la tabla Admin y los mapea a objetos AdminResponse
                var result = _dbContext.Admin.Select(c => new AdminResponse()
                {
                    Id = c.Id,
                    cedula = c.cedula,
                    nickName = c.nickName,
                    status = c.status,
                    email = c.email,
                });

                // Ejecuta la consulta y devuelve los resultados como una lista
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarAdminQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException(ex.Message);
            }
        }
    }
}
