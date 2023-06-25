using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    public class RecibirArchivoConciliacionCommandHandler : IRequestHandler<RecibirArchivoConciliacionCommand, string>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<RecibirArchivoConciliacionCommandHandler> _logger;
        private readonly FirebaseStorage _firebaseStorage;

        public RecibirArchivoConciliacionCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<RecibirArchivoConciliacionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _firebaseStorage = new FirebaseStorage("pagalotodoucab-927ea.appspot.com");
        }

        public async Task<string> Handle(RecibirArchivoConciliacionCommand request, CancellationToken cancellationToken)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                IFormFile csvContent = request._file;
                PrestadorServicioEntity prestador = await _dbContext.PrestadorServicio.FindAsync(request._idprestadorservicio) ?? throw new InvalidOperationException();

                string dowloadURL = "";
                using (StreamReader reader = new StreamReader(csvContent.OpenReadStream()))
                {
                    string csvContentString = await reader.ReadToEndAsync();
                    using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(csvContentString)))
                    {
                        string firebaseStoragePath = "archivos_recibidos/" + prestador.nickName + "-" + DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss") + ".csv";
                        var load = await _firebaseStorage.Child(firebaseStoragePath).PutAsync(memoryStream);
                        dowloadURL = await _firebaseStorage.Child(firebaseStoragePath).GetDownloadUrlAsync();
                    }
                }

                if (string.IsNullOrEmpty(dowloadURL))
                    throw new CustomException(500, "Error en guardado de archivo en Firebase");

                _dbContext.ArchivoFirebase.Add(new ArchivoFirebaseEntity()
                {
                    Id = Guid.NewGuid(),
                    urlFirebase = dowloadURL,
                    prestadorServicio = prestador,
                    tipoArchivo = ArchivoFirebase.recibido
                });

                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("RecibirArchivoConciliacionCommandHandler.HandleAsync {Response}", dowloadURL);

                return dowloadURL;
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, "Error RecibirArchivoConciliacionCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error RecibirArchivoConciliacionCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);
            }
        }
    }
}
