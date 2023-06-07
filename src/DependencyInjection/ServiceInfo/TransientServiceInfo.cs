namespace DependencyInjection.ServiceInfo;

public class TransientServiceInfo : IServiceInfo
{
    public TransientServiceInfo(Type? implementedInterface, Type actualType)
    {
        ImplementedInterface = implementedInterface;
        ActualType = actualType;
        Dependencies = ActualType.GetDependencies();
    }
    
    public ServiceLifetime Lifetime => ServiceLifetime.Transient;
    public Type? ImplementedInterface { get; }
    public Type ActualType { get; }
    public object? Instance {
        get
        {
            if (!Dependencies?.Any() ?? false)
            {
                return Activator.CreateInstance(ActualType);
            }

            return null;
        }
    }
    public IEnumerable<Type>? Dependencies { get; }
    public object ResolveDeps(object?[]? args)
    {
        return Activator.CreateInstance(ActualType, args);
    }
}