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
        return (T)GetService(typeof(T));
    }
    
    public object GetService(Type t)
    {
        var service = _services[t];
        if (service.Instance != null) return service.Instance;

        var deps = service.Dependencies;
        var args = deps?.Select(GetService).ToArray();
        
        service.ResolveDeps(args);
        if (service.Instance != null) return service.Instance;
        
        return default;

    }
}