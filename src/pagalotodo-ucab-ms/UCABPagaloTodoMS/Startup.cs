using System.Reflection;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Infrastructure.Database;
using UCABPagaloTodoMS.Infrastructure.Settings;
using UCABPagaloTodoMS.Providers.Implementation;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MediatR;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Handlers.Commands;
using UCABPagaloTodoMS.Application.Handlers.Commands.Patch;
using UCABPagaloTodoMS.Application.Mailing;
using UCABPagaloTodoMS.Core.Services;
using UCABPagaloTodoMS.Infrastructure.Services;
using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS;

[ExcludeFromCodeCoverage]
public class Startup
{
    private AppSettings _appSettings;
    private readonly string _allowAllOriginsPolicy = "AllowAllOriginsPolicy";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        VersionNumber = "v" + Assembly.GetEntryAssembly()!
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;
        Folder = "docs";
    }
    private string Folder { get; }
    private string VersionNumber { get; }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        //RabbitMQ
        services.AddTransient<IRabbitMQService, RabbitMQService>();
        services.AddTransient<IRabbitMQProducer, RabbitMQProducer>();
        services.AddHostedService<RabbitMqConsumerConciliacionHS>();
        services.AddHostedService<RabbitMqConsumerVerificacionHS>();
        //---------
        services.AddMediatR(typeof(RecibirArchivoConciliacionCommandHandler));

        services.AddCors(options =>
        {
            options.AddPolicy(_allowAllOriginsPolicy,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
        });
    
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Ingrese el token JWT en este formato: Bearer {token}",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            // Especificar que los métodos o controladores que requieren autenticación deben utilizar este esquema de seguridad
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                        },
                    new List<string>()
                    }

                });

        });
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = Configuration["Jwt:Audience"],
                ValidIssuer = Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            };
        });
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var appSettingsSection = Configuration.GetSection("AppSettings");
        _appSettings = appSettingsSection.Get<AppSettings>();
        services.Configure<AppSettings>(appSettingsSection);
        services.AddTransient<IUCABPagaloTodoDbContext, UCABPagaloTodoDbContext>();
        var emailConfig = Configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddSingleton(emailConfig);
        services.AddEndpointsApiExplorer();

        services.AddProviders(Configuration, Folder, _appSettings, environment);

        services.AddMediatR(
       typeof(ConsultarValoresQueryHandler).GetTypeInfo().Assembly);

        


    }
    
    public void Configure(IApplicationBuilder app)
    {
        var appSettingsSection = Configuration.GetSection("AppSettings");
        _appSettings = appSettingsSection.Get<AppSettings>();

        app.UseHttpsRedirection();
        app.UseRouting();

        if (_appSettings.RequireSwagger)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "/{documentname}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./" + Folder + "/swagger.json", $"UCABPagaloTodo Microservice ({VersionNumber})");
                c.InjectStylesheet(_appSettings.SwaggerStyle);
                c.DisplayRequestDuration();
                c.RoutePrefix = string.Empty;
            });


        }


        if (_appSettings.RequireAuthorization)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
        app.UseAuthentication();
        app.UseAuthorization();

        if (_appSettings.RequireControllers)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health/ready",
                    new HealthCheckOptions { Predicate = check => check.Tags.Contains("ready") });
                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });
            });
        }
    }
}
