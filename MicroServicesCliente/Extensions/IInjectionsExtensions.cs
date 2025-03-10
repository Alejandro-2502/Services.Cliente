using Cliente.Application.UserHistory.Comands.CreateCliente;
using Cliente.Application.UserHistory.Comands.DeleteCliente;
using Cliente.Application.UserHistory.Comands.UpdateCliente;
using Cliente.Application.UserHistorys.Commands.CreateCliente;
using Cliente.Application.UserHistorys.Commands.UpdateCliente;
using Cliente.Application.UserHistorys.Commons;
using Cliente.Domain.Interfaces;
using Cliente.Infrastructure.Repository;
using Cliente.Infrastructure.UnitOfWork;

namespace MicroServicioCliente.Extensions
{
    public static class IInjectionsExtensions
    {
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClienteCommandRepository, ClienteCommandRepository>();
            services.AddScoped<IClienteQuerysRepository, ClienteQuerysRepository>();
            
            //Registro Mediator de cada clase donde se utiliza MediatR
            services.AddMediatR(cfg => {cfg.RegisterServicesFromAssembly(typeof(CreateClienteHandler).Assembly);});
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteClienteHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateClienteHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoggerHandler).Assembly));

            //Registro Mediator de cada clase de validaciones donde se utiliza MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateValidationsClienteHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateValidationsClienteHandler).Assembly));

            services.AddSingleton<CreateClienteValidator>();
            services.AddSingleton<UpdateClienteValidator>();
            
            services.AddScoped<ResponseHttp>();
            
            return services;
        }
    }
}
