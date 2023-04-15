using MiniMart.Domain.Interfaces.Repositories;
using MiniMart.Domain.Interfaces;
using System.Reflection;
using MiniMart.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MiniMart.Infrastructure.Data.Repositories;
using MiniMart.API.Services;
using MiniMart.Domain.DomainEvents;
using MediatR;

namespace MiniMart.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddImplementationsAsInterfaces(this IServiceCollection services
            , Type interfaceType
            , params Type[] implementationAssemblyTypes)
        {
            foreach (var assemblyType in implementationAssemblyTypes)
            {
                var implementationTypes = Assembly
                    .GetAssembly(assemblyType)
                    .GetTypes()
                    .Where(_ =>
                        _.IsClass
                        && !_.IsAbstract
                        && !_.IsInterface
                        && _.GetInterface(interfaceType.Name) != null
                        && !_.IsGenericType
                    );

                foreach (var implementationType in implementationTypes)
                {
                    var mainInterfaces = implementationType
                        .GetInterfaces()
                        .Where(_ => _.GenericTypeArguments.Count() == 0);

                    foreach (var mainInterface in mainInterfaces)
                    {
                        services.AddScoped(mainInterface, implementationType);
                    }
                }
            }
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetSection("ConnectionString").Value);
            });
            return services;
        }

        public static IServiceCollection AddGenericRepositories(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddImplementationsAsInterfaces(
                    typeof(IGenericRepository<>)
                    , typeof(GenericRepository<>)
                );
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<UserService>();

            //services.AddScoped(s =>
            //{
            //    return new PaymentService(configuration);
            //});
            return services;
        }


        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        public static IServiceCollection RegisterMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(CreateUserDomainEvent).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(CreateUserDomainEvent).GetTypeInfo().Assembly);
            return services;
        }
    }
}
