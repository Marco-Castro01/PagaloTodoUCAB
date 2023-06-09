using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace UCABPagaloTodoMS.Application.Handlers.Commands.Patch
{
    /// <summary>
    /// Clase que maneja el comando para actualizar el estado de una deuda.
    /// </summary>
    public class UpdateDeudaStatusCommandHandler : IRequestHandler<UpdateDeudaStatusCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<UpdateDeudaStatusCommandHandler> _logger;

        public UpdateDeudaStatusCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<UpdateDeudaStatusCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Maneja el comando para actualizar el estado de una deuda.
        /// </summary>
        /// <param name="request">Comando para actualizar el estado de una deuda</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Identificador de la deuda actualizada</returns>
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

        /// <summary>
        /// Maneja asincrónicamente el comando para actualizar el estado de una deuda.
        /// </summary>
        /// <param name="request">Comando para actualizar el estado de una deuda</param>
        /// <returns>Identificador de la deuda actualizada</returns>
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
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error ModificarStatusDeudaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException("Error al modificar El estatus de la deuda",ex);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ModificarStatusDeudaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;

            }
        }
    }
}