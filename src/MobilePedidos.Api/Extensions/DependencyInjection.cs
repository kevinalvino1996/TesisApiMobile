using FluentValidation;
using MobilePedidos.Application.Interfaces;
using MobilePedidos.Application.Services;
using MobilePedidos.Application.Validators.Cliente;
using MobilePedidos.Application.Validators.Login;
using MobilePedidos.Domain.Interfaces;
using MobilePedidos.Infraestructure.DbConnection;
using MobilePedidos.Infraestructure.Repositories;

namespace MobilePedidos.Api.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Conexión y Repositorios
            services.AddScoped<DapperContext>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Servicios de Aplicación
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IAuthService, AuthService>();

            // Validadores
            services.AddValidatorsFromAssembly(typeof(ClienteBuscarDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(ClienteRegistrarDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(ClienteEditarDtoValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(LoginDtoValidator).Assembly);
            return services;
        }
    }
}
