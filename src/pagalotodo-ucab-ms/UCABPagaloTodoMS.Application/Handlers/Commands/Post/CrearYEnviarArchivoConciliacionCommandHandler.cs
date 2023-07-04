using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Firebase.Auth.Providers;
using Firebase.Storage;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft;
using Newtonsoft.Json;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;
using UCABPagaloTodoMS.Infrastructure.Migrations;
using ValidationException = FluentValidation.ValidationException;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Clase que maneja el comando para agregar un prestador.
    /// </summary>
    public class CrearYEnviarArchivoConciliacionCommandHandler : IRequestHandler<CrearYEnviarArchivoConciliacionCommand, string>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<CrearYEnviarArchivoConciliacionCommandHandler> _logger;
        private readonly string api_key="tZJQqp4hTDWoAP67m8o1NCxaSGgaTEg8w4JdiehS";
        private readonly string Bucket = "pagalotodoucab-db427.appspot.com/";
        private readonly string authEmail="castroo8a2@gmail.com";
        private readonly string authPassword="123456";
        /// <summary>
        /// Constructor de la clase AgregarPrestadorCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public CrearYEnviarArchivoConciliacionCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<CrearYEnviarArchivoConciliacionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            
        }

        /// <summary>
        /// Maneja el comando para agregar un prestador.
        /// </summary>
        /// <param name="request">Comando para agregar un prestador</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Identificador del prestador agregado</returns>
        public async Task<string> Handle(CrearYEnviarArchivoConciliacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleAsync(request._idprestadorservicio);
            }
            catch (CustomException)
            {
                throw;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Maneja asincrónicamente el comando para agregar un prestador.
        /// </summary>
        /// <param name="request">Comando para agregar un prestador</param>
        /// <returns>Identificador del prestador agregado</returns>
        /// 
        private async Task<string> HandleAsync(Guid request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                
                _logger.LogInformation("AgregarPrestadorCommandHandler.HandleAsync {Request}", request);
                StringBuilder csvContent = new StringBuilder();
                var FirebaseStorage = new FirebaseStorage("pagalotodoucab-927ea.appspot.com");
                var dowloadURL = "";

                PrestadorServicioEntity prestador = await _dbContext.PrestadorServicio.FindAsync(request) ?? throw new InvalidOperationException();
                var fechaUltimoCierre = await _dbContext.ArchivoFirebase
                    .Where(c => c.prestadorServicio.Id == prestador.Id && c.deleted==false)
                    .OrderByDescending(c=>c.CreatedAt)
                    .Select(c => c.CreatedAt)
                    .FirstOrDefaultAsync();
                if (fechaUltimoCierre == default(DateTime))
                    fechaUltimoCierre = new DateTime(2000, 1, 1, 10, 30, 0);

                if (prestador == null)
                    throw new CustomException("Prestador de servicio no existente");

                List<ServicioEntity> listServicio = _dbContext.Servicio.Where(c => c.PrestadorServicio.Id == prestador.Id).ToList();
                if (listServicio == null || listServicio.Count == 0)
                    throw new CustomException("El prestador no posee servicios");

                foreach (var servicio in listServicio)
                {
                    servicio.ServicioCampo = _dbContext.ServicioCampo
                        .Where(c => c.ServicioId == servicio.Id )
                        .Include(o => o.Campo).ToList();
                    if (servicio.ServicioCampo == null || servicio.ServicioCampo.Count == 0)
                        return "No posee servicios asociados";

                    string campos = string.Join(";", servicio.ServicioCampo.Select(sc => sc.Campo?.Nombre));
                    if (string.IsNullOrEmpty(campos))
                        throw new CustomException(500,"ERROR: No tiene nombre el campo");

                    if (servicio.formatoDePagos!=null)
                    {
                        List<CamposPagosRequest> camposP = CamposPersonalizados(servicio);
                        campos ="identificador;check;"+campos+";"+ConvertCamposPInStringHead(camposP)+"monto";
                    }
                    else
                    {
                        campos ="identificador;check;"+campos+";monto";
                    }
                    
                    csvContent.AppendLine("Servicio; " + servicio.name);
                    csvContent.AppendLine(campos);
                    List<PagoEntity> listPagos = _dbContext.Pago.Where(c => c.servicio != null && c.servicio.Id == servicio.Id && c.CreatedAt>=fechaUltimoCierre).Include(o => o.consumidor).ToList();

                    if (listPagos == null || listPagos.Count == 0)
                    {
                        foreach (var pago in listPagos)
                        { 
                            StringBuilder datosString = new StringBuilder();
                            datosString.Append(pago.Id+";en espera;");
                            foreach (var campo in campos.Split(";"))
                            {
                                if (campo.Equals("nombre"))
                                    datosString.Append(pago.consumidor.name + " " + pago.consumidor.lastName + ";");
                                if (campo.Equals("cedula"))
                                    datosString.Append(pago.consumidor.cedula + ";");
                                if (campo.Equals("email"))
                                    datosString.Append(pago.consumidor.email + ";");
                                if (campo.Equals("nickname"))
                                    datosString.Append(pago.consumidor.nickName + ";");
                            } 
                        
                            if (servicio.formatoDePagos!=null){
                                List<CamposPagosRequest> camposPa =JsonConvert.DeserializeObject<List<CamposPagosRequest>>(pago.formatoPago);
                                datosString.Append(ConvertCamposPInStringBody(camposPa));
                            }
                        
                            datosString.Append(pago.valor.ToString());
                            csvContent.AppendLine(datosString.ToString());
                        }
                    }
                }

                using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent.ToString())))
                {
                    string firebaseStoragePath = "archivos_enviados/" + prestador.nickName + "-" + DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss") + ".csv";
                    var load = await FirebaseStorage.Child(firebaseStoragePath).PutAsync(memoryStream);
                    dowloadURL = await FirebaseStorage.Child(firebaseStoragePath).GetDownloadUrlAsync();
                }

                if (dowloadURL == "")
                    throw new CustomException(500, "Error en guardado de archivo en firebase");
                
                _dbContext.ArchivoFirebase.Add(new ArchivoFirebaseEntity()
                {
                    Id = new Guid(),
                    urlFirebase = dowloadURL,
                    prestadorServicio = prestador,
                    tipoArchivo = ArchivoFirebase.enviado
                    
                });

                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarPrestadorCommandHandler.HandleAsync {Response}", dowloadURL);
                return dowloadURL;
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, "Error AgregarPrestadorCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarPrestadorCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);
            }
        }

        public string ConvertCamposPInStringHead(List<CamposPagosRequest> campos)
        {
            string resultado = "";
            foreach (var campo in campos )
            {
                if (campo.inCOnciliacion) // Verifica la condición adicional
                {
                    resultado += campo.Nombre + ";"; // Agrega el atributo al string
                }
            }
            return resultado;
        }
        public string ConvertCamposPInStringBody( List<CamposPagosRequest>campos)
        {
            string resultado = "";
            foreach (var campo in campos )
            {
                if (campo.inCOnciliacion) // Verifica la condición adicional
                {
                    resultado += campo.contenido + ";"; // Agrega el atributo al string
                }
            }
            return resultado;
        }
        public List<CamposPagosRequest> CamposPersonalizados(ServicioEntity servicio)
        {

            List<CamposPagosRequest> listaObjetos = JsonConvert.DeserializeObject<List<CamposPagosRequest>>(servicio.formatoDePagos);
            return listaObjetos;
        }
        
    }
    
    



    
    
}
