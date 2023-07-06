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
    /// <summary>
    /// Manejador del comando para eliminar un servicio de prueba.
    /// </summary>
    public class DeleteServicioPruebaCommandHandler : IRequestHandler<DeleteServicioPruebaCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<DeleteServicioPruebaCommandHandler> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de la clase DeleteServicioPruebaCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos de UCABPagaloTodo.</param>
        /// <param name="logger">Logger para el manejo de registros.</param>
        public DeleteServicioPruebaCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<DeleteServicioPruebaCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Maneja la ejecución del comando para eliminar un servicio de prueba.
        /// </summary>
        /// <param name="request">Comando de eliminación de servicio de prueba.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>El identificador del servicio eliminado.</returns>
        public async Task<Guid> Handle(DeleteServicioPruebaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("EliminarServicioPruebaCommandHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        private async Task<Guid> HandleAsync(DeleteServicioPruebaCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("EliminarServicioPruebaCommandHandler.HandleAsync {Request}", request);
                var entity = ServicioMapper.MapRequestDeleteEntity(request,_dbContext);
                ServicioValidator ServicioValidator = new ServicioValidator();
                ValidationResult result = await ServicioValidator.ValidateAsync(entity);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                _dbContext.Servicio.Update(entity);
                _dbContext.DbContext.SaveChanges();

                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("EliminarServicioPruebaCommandHandler.HandleAsync {Response}", id);
                return id;
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error EliminarServicioPruebaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException("Error al eliminar Servicio", ex);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error EliminarServicioPruebaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);

            }
        }
    }
}
