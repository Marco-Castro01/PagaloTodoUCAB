using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    public class CambiarContrasenaCommandHandler : IRequestHandler<CambiarContrasenaCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<CambiarContrasenaCommandHandler> _logger;

        public CambiarContrasenaCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<CambiarContrasenaCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Guid> Handle(CambiarContrasenaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("CambiarContrasenaCommand.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<Guid> HandleAsync(CambiarContrasenaCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("CambiarContrasenaCommand.HandleAsync {Request}", request);

                var user = _dbContext.Usuarios.Where(u => u.PasswordResetToken == request._request.token).FirstOrDefault();
              if (user == null || user.ResetTokenExpires < DateTime.Now)
               {
                    _logger.LogWarning("Ha ocurrido un error Token invalido");
                    throw new ArgumentNullException(nameof(user));


                }
                using (var hash = new HMACSHA512()) //esto es para el password
                {
                    user.passwordSalt = hash.Key;
                    user.passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request._request.password));
                }
                    user.ResetTokenExpires = null;
                    user.PasswordResetToken = null;
                _dbContext.Usuarios.Update(user);
                _dbContext.DbContext.SaveChanges();
                var id = user.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("CambiarContrasenaCommand.HandleAsync {Response}", id);
                return id;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }
    }
}
