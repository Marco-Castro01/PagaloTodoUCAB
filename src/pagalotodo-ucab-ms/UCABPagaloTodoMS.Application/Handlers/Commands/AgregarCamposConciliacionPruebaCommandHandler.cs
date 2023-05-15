using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    public class AgregarCamposConciliacionPruebaCommandHandler : IRequestHandler<AgregarCamposConciliacionPruebaCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AgregarCamposConciliacionPruebaCommandHandler> _logger;

        public AgregarCamposConciliacionPruebaCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AgregarCamposConciliacionPruebaCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Guid> Handle(AgregarCamposConciliacionPruebaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("AgregarCamposConciliacionPruebaCommandHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private async Task<Guid> HandleAsync(AgregarCamposConciliacionPruebaCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarCamposConciliacionPruebaCommandHandler.HandleAsync {Request}" , request);
                var entity = CamposConciliacionMapper.MapRequestEntity(request._request);
                _dbContext.CamposConciliacion.Add(entity);
                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarCamposConciliacionPruebaCommandHandler.HandleAsync {Response}", id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarCamposConciliacionPruebaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }
    }
}