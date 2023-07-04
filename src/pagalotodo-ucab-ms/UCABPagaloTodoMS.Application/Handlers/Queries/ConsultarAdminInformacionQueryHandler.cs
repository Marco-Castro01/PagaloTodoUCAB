using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.CustomExceptions;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarAdminInformacionQueryHandler : IRequestHandler<GetInfoAdminQuery, AdminResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarAdminInformacionQueryHandler> _logger;

        public ConsultarAdminInformacionQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarAdminInformacionQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<AdminResponse> Handle(GetInfoAdminQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarAdminQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return HandleAsync(request);
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

        private async Task<AdminResponse> HandleAsync(GetInfoAdminQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarAdminQueryHandler.HandleAsync");

                // Consulta los registros de la tabla Admin y los mapea a objetos AdminResponse
                var result = _dbContext.Admin
                    .FirstOrDefault(x => x.deleted == false && x.Id == request._idAdmin);

                if (result == null)
                    throw new CustomException(404, "Admin no existente");
                    
                var adminResponse = new AdminResponse()
                {
                    Id = result.Id,
                    cedula = result.cedula,
                    nickName = result.nickName,
                    status = result.status,
                    email = result.email,
                };

                return adminResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarAdminQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException(ex.Message);
            }
        }
    }
}
