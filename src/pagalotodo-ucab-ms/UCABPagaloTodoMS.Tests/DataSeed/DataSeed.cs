using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Core.Database;
using MockQueryable.Moq;
using Moq;
using UCABPagaloTodoMS.Infrastructure.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Infrastructure.Migrations;
using Castle.Components.DictionaryAdapter;
using Bogus.DataSets;
using System.Text;
using Microsoft.Data.SqlClient.Server;

namespace UCABPagaloTodoMS.Tests.DataSeed
{
    public static class DataSeed
    {
        public static Mock<DbSet<AdminEntity>> mockSetAdminEntity = new Mock<DbSet<AdminEntity>>();
        public static Mock<DbSet<BaseEntity>> mockSetBaseEntity = new Mock<DbSet<BaseEntity>>();
        public static Mock<DbSet<CamposConciliacionEntity>> mockSetCamposConciliacionEntity = new Mock<DbSet<CamposConciliacionEntity>>();
    public static Mock<DbSet<ConsumidorEntity>> mockSetConsumidorEntity = new Mock<DbSet<ConsumidorEntity>>();
    public static Mock<DbSet<PagoEntity>> mockSetSPagoEntity = new Mock<DbSet<PagoEntity>>();
    public static Mock<DbSet<PrestadorServicioEntity>> mockSetPrestadorServicioEntity = new Mock<DbSet<PrestadorServicioEntity>>();
    public static Mock<DbSet<ServicioEntity>> mockSetServicioEntity = new Mock<DbSet<ServicioEntity>>();
    public static Mock<DbSet<UsuarioEntity>> mockSetUsuarioEntity = new Mock<DbSet<UsuarioEntity>>();
    public static Mock<DbSet<ValoresEntity>> mockSetValoresEntity = new Mock<DbSet<ValoresEntity>>();
        private static byte[] passwordHash;

        public static void SetupDbContextData(this Mock<IUCABPagaloTodoDbContext> mockContext)
        {

            var Admin = new List<AdminEntity>
            {
                new AdminEntity
                {
                    cedula = "123456789"
                },


            };

            var Base = new List<BaseEntity>
            {
                 new BaseEntity
                {
                    Id = new Guid("75fe535f-e5bc-4457-be0e-3c12302266a5"),
                    CreatedBy = "Bismarck",
                    UpdatedAt = DateTime.Now,
                    
                },



            };
            var CamposConciliacion = new List<CamposConciliacionEntity>
            {
                 new CamposConciliacionEntity
                {
                    Nombre = "prueba",
                    Longitud = 2,
                    Servicio = new List<ServicioEntity> {}
                },



            };
            var Consumidor = new List<ConsumidorEntity>
            {
                 new ConsumidorEntity
                {
                    name = "Prueba1",
                    lastName = "Arteaga",
                    cedula = "123456789",
                    pago =  new List<PagoEntity> {}

                },



            };
            var Prestador = new List<PrestadorServicioEntity>
            {
                 new PrestadorServicioEntity
                {
                    rif = "123456789",
                    Servicio = new List<ServicioEntity> {},
                },



            };
            var Servicio = new List<ServicioEntity>
            {
                 new ServicioEntity
                {
                    name = "prueba",
                    accountNumber = "1",
                    Pago = new List<PagoEntity>{},
                    CamposConciliacion = new List<CamposConciliacionEntity>{},
                    PrestadorServicio = new PrestadorServicioEntity()
                },



            };



            /*var Usuario = new List<UsuarioEntity>
            {
                 new UsuarioEntity
                {

                  email = "bismarck@gmail.com",
                  name = "bismarck",
                  cedula = "123456789",
                  status = true
                },

            };
            */

            var Pago = new List<PagoEntity>
            {
                 new PagoEntity
                {
                    valor = 1.2 , 
                    servicio =  new ServicioEntity(),
                    consumidor = new ConsumidorEntity(),
                },



            };
         
         
            var Valores = new List<ValoresEntity>
            {
                 new ValoresEntity
                {
                    Nombre = "Bismarck",
                    Apellido = "Ponce",
                    Identificacion = ""
                },



            };

            mockContext.Setup(x => x.Admin).Returns(mockSetAdminEntity.Object);
            mockContext.Setup(c => c.Admin).Returns(Admin.AsQueryable().BuildMockDbSet().Object);
            mockContext.Setup(c => c.SaveEfContextChanges("APP", It.IsAny<CancellationToken>())).ReturnsAsync(true);

            mockContext.Setup(x => x.Base).Returns(mockSetBaseEntity.Object);
            mockContext.Setup(c => c.Base).Returns(Base.AsQueryable().BuildMockDbSet().Object);
            mockContext.Setup(c => c.SaveEfContextChanges("APP", It.IsAny<CancellationToken>())).ReturnsAsync(true);

            mockContext.Setup(x => x.CamposConciliacion).Returns(mockSetCamposConciliacionEntity.Object);
            mockContext.Setup(c => c.CamposConciliacion).Returns(CamposConciliacion.AsQueryable().BuildMockDbSet().Object);
            mockContext.Setup(c => c.SaveEfContextChanges("APP", It.IsAny<CancellationToken>())).ReturnsAsync(true);

            mockContext.Setup(x => x.Consumidor).Returns(mockSetConsumidorEntity.Object);
            mockContext.Setup(c => c.Consumidor).Returns(Consumidor.AsQueryable().BuildMockDbSet().Object);
            mockContext.Setup(c => c.SaveEfContextChanges("APP", It.IsAny<CancellationToken>())).ReturnsAsync(true);

            mockContext.Setup(x => x.PrestadorServicio).Returns(mockSetPrestadorServicioEntity.Object);
            mockContext.Setup(c => c.PrestadorServicio).Returns(Prestador.AsQueryable().BuildMockDbSet().Object);
            mockContext.Setup(c => c.SaveEfContextChanges("APP", It.IsAny<CancellationToken>())).ReturnsAsync(true);

            mockContext.Setup(x => x.Servicio).Returns(mockSetServicioEntity.Object);
            mockContext.Setup(c => c.Servicio).Returns(Servicio.AsQueryable().BuildMockDbSet().Object);
            mockContext.Setup(c => c.SaveEfContextChanges("APP", It.IsAny<CancellationToken>())).ReturnsAsync(true);

            mockContext.Setup(x => x.Pago).Returns(mockSetSPagoEntity.Object);
            mockContext.Setup(c => c.Pago).Returns(Pago.AsQueryable().BuildMockDbSet().Object);
            mockContext.Setup(c => c.SaveEfContextChanges("APP", It.IsAny<CancellationToken>())).ReturnsAsync(true);

            mockContext.Setup(x => x.Valores).Returns(mockSetValoresEntity.Object);
            mockContext.Setup(c => c.Valores).Returns(Valores.AsQueryable().BuildMockDbSet().Object);
            mockContext.Setup(c => c.SaveEfContextChanges("APP", It.IsAny<CancellationToken>())).ReturnsAsync(true);

        }
    }
}