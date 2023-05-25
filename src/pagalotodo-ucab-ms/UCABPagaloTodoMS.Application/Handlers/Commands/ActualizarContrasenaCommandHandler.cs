using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    public class ActualizarContrasenaCommandHandler : IRequestHandler<ActualizarContrasenaCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
    private readonly ILogger<ActualizarContrasenaCommandHandler> _logger;

    public ActualizarContrasenaCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ActualizarContrasenaCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Guid> Handle(ActualizarContrasenaCommand request, CancellationToken cancellationToken)
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

    private async Task<Guid> HandleAsync(ActualizarContrasenaCommand request)
    {
        var transaccion = _dbContext.BeginTransaction();
        try
        {
            _logger.LogInformation("ActualizarContrasenaCommand.HandleAsync {Request}", request);

            var user = _dbContext.Usuarios.Where(u => u.email == request._request.email).FirstOrDefault();
            using (var hash = new HMACSHA512()) //esto es para el password
            {
                user.passwordSalt = hash.Key;
                user.passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request._request.Password));
            }
            _dbContext.Usuarios.Update(user);
            _dbContext.DbContext.SaveChanges();
            var id = user.Id;
            await _dbContext.SaveEfContextChanges("APP");
            transaccion.Commit();
            _logger.LogInformation("ActualizarContrasenaCommand.HandleAsync {Response}", id);
            return id;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error ActualizarContrasenaCommand.HandleAsync. {Mensaje}", ex.Message);
            transaccion.Rollback();
            throw;
        }
    }
}
}
