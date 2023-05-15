﻿using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarCamposConciliacionQueryHandler : IRequestHandler<ConsultarCamposConciliacionPruebaQuery, List<CamposConciliacionResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarCamposConciliacionQueryHandler> _logger;

        public ConsultarCamposConciliacionQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarCamposConciliacionQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<CamposConciliacionResponse>> Handle(ConsultarCamposConciliacionPruebaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarCamposConciliacionQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return HandleAsync();
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarCamposConciliacionQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<List<CamposConciliacionResponse>> HandleAsync()
        {
            try
            {
                _logger.LogInformation("ConsultarCamposConciliacionQueryHandler.HandleAsync");

                var result = _dbContext.CamposConciliacion.Select(c => new CamposConciliacionResponse()
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Longitud = c.Longitud
                });

                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarCamposConciliacionQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }
        }
    }
}