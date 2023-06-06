using DependencyInjection.ServiceInfo;

namespace DependencyInjection;

public class ServiceCollection
{
    private List<IServiceInfo> _services = new();

    public ServiceCollection()
    {
    }


    public DiContainer Build()
    {
        return new DiContainer(_services);
    }

    public void AddSingleton<T>(T instance)
    {
        _services.Add(new SingletonServiceInfo(null, typeof(T), instance));
    }
    
    public void AddSingleton<T>()
    {
        _services.Add(new SingletonServiceInfo(null, typeof(T)));
    }
    
    public void AddSingleton<TInterface, T>(T instance) where T : TInterface
    {
        _services.Add(new SingletonServiceInfo(null, typeof(T), instance));
    }
    
    public void AddSingleton<TInterface,T>() where T : TInterface
    {
        _services.Add(new SingletonServiceInfo(null, typeof(T)));
    }

}