using FluentValidation;
using GreenPipes;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    public class UpdateDeudaStatusCommandHandler : IRequestHandler<UpdateDeudaStatusCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<UpdateDeudaStatusCommandHandler> _logger;

        public UpdateDeudaStatusCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<UpdateDeudaStatusCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Guid> Handle(UpdateDeudaStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("ModificarStatusDeuda.Handle: Request nulo.");
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

        private async Task<Guid> HandleAsync(UpdateDeudaStatusCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("ModificarStatusDeudaCommandHandler.HandleAsync {Request}" , request);
                var entity = DeudaMapper.MapRequestEntity(request._request,_dbContext);
                
                DeudaValidator deudaValidator = new DeudaValidator();
                if (deudaValidator == null) throw new CustomException(nameof(deudaValidator));
                ValidationResult result = await deudaValidator.ValidateAsync(entity);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                _dbContext.Deuda.Update(entity);
                await _dbContext.DbContext.SaveChangesAsync();

                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("ModificarStatusDeudaCommandHandler.HandleAsync {Response}", id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ModificarStatusDeudaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);

            }
        }
    }
}