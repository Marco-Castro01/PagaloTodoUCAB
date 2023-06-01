using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Infrastructure.Database;
using ValidationException = FluentValidation.ValidationException;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    public class AgregarPagoPorValidacionCommandHandler : IRequestHandler<AgregarPagoPorValidacionCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AgregarPagoPorValidacionCommandHandler> _logger;
        private readonly IMediator _mediator;


        public AgregarPagoPorValidacionCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AgregarPagoPorValidacionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Guid> Handle(AgregarPagoPorValidacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Request == null)
                {
                    _logger.LogWarning("AgregarPagoDirectoCommandHandler.Handle: Request nulo.");
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

        private async Task<Guid> HandleAsync(AgregarPagoPorValidacionCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarPagoDirectoCommandHandler.HandleAsync {Request}" , request);
                var entity = PagoMapper.MapRequestPorValidacionEntity(request.Request,_dbContext);
                PagoDirectoValidator pagoDirectoValidator = new PagoDirectoValidator();
                ValidationResult result = await pagoDirectoValidator.ValidateAsync(entity);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                
                _dbContext.Pago.Add(entity);
                var query = new UpdateDeudaStatusCommand(new UpdateDeudaStatusRequest()
                {
                    
                    idDeuda = request.Request.IdDeuda,
                    deudaStatus = true
                    
                });
                var resp = await _mediator.Send(query);

                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarPagoDirectoCommandHandler.HandleAsync {Response}", id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarPagoDirectoCommandHandlerAgregarPagoCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);
            }
        }
    }
}