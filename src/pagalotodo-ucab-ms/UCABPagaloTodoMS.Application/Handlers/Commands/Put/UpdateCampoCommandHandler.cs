using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    public class UpdateCampoCommandHandler : IRequestHandler<UpdateCampoCommand, string>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<UpdateCampoCommandHandler> _logger;

        public UpdateCampoCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<UpdateCampoCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<string> Handle(UpdateCampoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("EditarUsuarioCommand.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return await HandleAsync(request);
            }
            catch (CustomException)
            {
                throw;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> HandleAsync(UpdateCampoCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("EditarUsuarioCommand.HandleAsync {Request}", request);

                var campo = _dbContext.CamposConciliacion
                    .FirstOrDefault(x=>x.Id==request._idCampo && x.deleted==false);
                if (campo == null)
                    throw new CustomException(404 ,"Campo no existente");
                campo.Longitud = request._request.Longitud;
                campo.Nombre = request._request.Nombre;
                CamposConciliacionValidator campoValidator = new CamposConciliacionValidator();
                ValidationResult result = await campoValidator.ValidateAsync(campo);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                _dbContext.CamposConciliacion.Update(campo);
                await _dbContext.SaveEfContextChanges("APP");

                transaccion.Commit();
                _logger.LogInformation("EditarUsuarioCommandHandler.HandleAsync {Response}", campo.Id);
                return "Modificacion exitosa";

            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, "Error AEditarUsuarioCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error EditarUsuarioCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);
            }
        }
    }
}
