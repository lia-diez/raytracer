using DependencyInjection.DefaultServices;
using DependencyInjection.ServiceInfo;

namespace DependencyInjection;

public static class ServicesExtensions
{
    public static void AddSingleton<T>(this ServiceCollection services, T instance)
    {
        services.Add(new SingletonServiceInfo(null, typeof(T), instance));
    }
    
    public static void AddSingleton<T>(this ServiceCollection services)
    {
        services.Add(new SingletonServiceInfo(null, typeof(T)));
    }
    
    public static void AddSingleton<TInterface, T>(this ServiceCollection services, T instance) where T : TInterface
    {
        services.Add(new SingletonServiceInfo(typeof(TInterface), typeof(T), instance));
    }
    
    public static void AddSingleton<TInterface,T>(this ServiceCollection services) where T : TInterface
    {
        services.Add(new SingletonServiceInfo(typeof(TInterface), typeof(T)));
    }
    
    public static void AddTransient<T>(this ServiceCollection services)
    {
        services.Add(new TransientServiceInfo(null, typeof(T)));
    }
    
    public static void AddTransient<TInterface,T>(this ServiceCollection services) where T : TInterface
    {
        services.Add(new TransientServiceInfo(typeof(TInterface), typeof(T)));
    }

    public static void AddArgs(this ServiceCollection services)
    {
        services.AddSingleton<IConfiguration, ConsoleArgsConfiguration>();
    }
}