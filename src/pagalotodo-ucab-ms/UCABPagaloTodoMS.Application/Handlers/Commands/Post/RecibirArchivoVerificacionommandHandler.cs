using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;
using UCABPagaloTodoMS.Core.Services;
using UCABPagaloTodoMS.Infrastructure.Services;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    public class RecibirArchivoVerificacionCommandHandler : IRequestHandler<RecibirArchivoVerificacionCommand, string>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<RecibirArchivoVerificacionCommandHandler> _logger;
        private readonly FirebaseStorage _firebaseStorage;
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public RecibirArchivoVerificacionCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<RecibirArchivoVerificacionCommandHandler> logger, IRabbitMQProducer rabbitMQProducer)
        {
            _dbContext = dbContext;
            _logger = logger;
            _rabbitMQProducer = rabbitMQProducer;
            _firebaseStorage = new FirebaseStorage("pagalotodoucab-927ea.appspot.com");

        }

        public async Task<string> Handle(RecibirArchivoVerificacionCommand request, CancellationToken cancellationToken)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                IFormFile csvContent = request._file;
                PrestadorServicioEntity prestador = _dbContext.PrestadorServicio.Include(o=>o.Servicio).FirstOrDefault(o=>o.Id==request._IdPrestador) ?? throw new InvalidOperationException();
                bool algunObjetoTieneId = prestador.Servicio != null && prestador.Servicio.Any(objeto => objeto.Id == request._idServicio && objeto.deleted==false);
                if (!algunObjetoTieneId)
                    throw new CustomException(403, "Error al conectar con servicio proporcionado");
                
                string dowloadURL = "";
                using (StreamReader reader = new StreamReader(csvContent.OpenReadStream()))
                {
                    string csvContentString = await reader.ReadToEndAsync();
                    using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(csvContentString)))
                    {
                        string firebaseStoragePath = "archivos_recibidos_verificacion/" + request._file.FileName+"-"+prestador.nickName + "-" + DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss") + ".csv";
                        var load = await _firebaseStorage.Child(firebaseStoragePath).PutAsync(memoryStream);
                        dowloadURL = await _firebaseStorage.Child(firebaseStoragePath).GetDownloadUrlAsync();
                    }
                }

                // Aquí se llaman las funciones que se encargarán de procesar el archivo recibido y enviarlo a la cola
                await ProcesarYEnviarAColaRabbit(request._file,request._idServicio);
                // Se inicia el consumidor en un nuevo hilo
    
                // Esperar a que el consumo de mensajes se haya iniciado antes de continuar

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

        public Task ProcesarYEnviarAColaRabbit(IFormFile file,Guid idServicio)
        {
            StringBuilder archivoConvert = new StringBuilder();
            archivoConvert.AppendLine("idServicio:"+idServicio);
            using (StreamReader reader = new StreamReader(file.OpenReadStream()))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (IsLineValid(line))
                    {
                        archivoConvert.AppendLine(line);

                    }

                }
            }
            _rabbitMQProducer.PublishMessageToVerificacion_Queue(archivoConvert.ToString());
            return Task.CompletedTask;
        }

        private bool IsLineValid(string line)
        {
            if (line.Contains("Servicio") || line.Contains("identificador"))
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(line);
        }
        
    }
}
