using AutoMapper;
using Cliente.Aplication.Configurations;
using Cliente.Aplication.Mappers;
using Cliente.Application.Configurations;
using Cliente.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace MicroServicioCliente.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection Configure(this IServiceCollection services, WebApplicationBuilder webApplicationBuilder, IConfiguration configuration)
    {
        ConfigHelper.ConfigSqlServer = configuration.GetSection(nameof(ConfigSqlServer)).Get<ConfigSqlServer>();
        ConfigHelper.ConfiLoggerFile = configuration.GetSection(nameof(ConfiLoggerFile)).Get<ConfiLoggerFile>();
        ConfigHelper.ConfigFormatos = configuration.GetSection(nameof(ConfigFormatos)).Get<ConfigFormatos>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        AddConfigureSwagger(services);
        services.AddMvc();
        services.AddInjections();
        services.AddAutoMapper();
        AddUseSqlServer(services, configuration);

        return services;
    }

    public static IServiceCollection AddConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            { Title = "MicroServicio Cliente", Version = "v1" });
        });

        return services;
    }
    public static IServiceCollection AddUseSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString(ConfigHelper.ConfigSqlServer.Connection)));

        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(typeof(MapperProfile));
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
        return services;
    }
}

