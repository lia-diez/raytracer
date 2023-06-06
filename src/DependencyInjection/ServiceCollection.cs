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
    
    
}