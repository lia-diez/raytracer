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

    public void Add(IServiceInfo serviceInfo)
    {
        _services.Add(serviceInfo);
    }



}