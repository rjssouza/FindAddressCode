using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using AutoMapper;
using Infrastructure.Helper;

namespace Infrastructure.Extension;

/// <summary>
/// Extension class for service collections
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register an array that implements a type
    /// </summary>
    /// <param name="services">Service collection used by dependency injection</param>
    /// <param name="assemblies">Assembly files that implement the interface</param>
    /// <param name="lifetime">Lifetime of the service (default Transient)</param>
    /// <typeparam name="T">Generic type that the assembly files implement</typeparam>
    public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
        foreach (var type in typesFromAssemblies)
            services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
    }

    /// <summary>
    /// Register an array that implements a type
    /// </summary>
    /// <param name="services">Service collection used by dependency injection</param>
    /// <param name="interfaceType">Generic type that the assembly files implement</param>
    /// <param name="assemblies">Assembly files that implement the interface</param>
    /// <param name="lifetime">Lifetime of the service (default Transient)</param>
    public static void RegisterAllTypes(this IServiceCollection services, Type interfaceType, Assembly[] assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        // var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(interfaceType)));
        var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Any(t => t.Name == interfaceType.Name)));

        foreach (var type in typesFromAssemblies){
            var implementedInterfaceType = type.GetInterfaces().Where(t => t.Name == interfaceType.Name).FirstOrDefault();
            if(implementedInterfaceType == null)
                continue;

            services.Add(new ServiceDescriptor(implementedInterfaceType, type, lifetime));
        }
    }

    /// <summary>
    /// Add automapper registration and profiles
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="typeAssembly">Assembly types</param>
    public static void AddAutoMapper(this IServiceCollection services, Assembly typeAssembly)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeAssembly);
        });
        var mapper = config.CreateMapper();
        MapperProfileHelper.Mapper = mapper;

        services.AddSingleton<IMapper>(t => mapper);
    }
}