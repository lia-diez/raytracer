using DependencyInjection.ServiceInfo;

namespace DependencyInjection;

public class DiContainer
{
    private Dictionary<Type, IServiceInfo> _services;

    public DiContainer(List<IServiceInfo> services)
    {
        _services = new Dictionary<Type, IServiceInfo>();
        foreach (var service in services)
        {
            _services[service.ImplementedInterface ?? service.ActualType] = service;
        }
    }
    
    public T GetService<T>()
    {
        var service = _services[typeof(T)];
        if (service.Instance == null)
        {
            //todo: 4eto
            return default;
        }
        
        return (T)service.Instance;
    }
}