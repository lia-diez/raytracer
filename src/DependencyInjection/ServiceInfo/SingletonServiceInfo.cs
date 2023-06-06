namespace DependencyInjection.ServiceInfo;

public class SingletonServiceInfo : IServiceInfo
{
    public SingletonServiceInfo(Type? implementedInterface, Type actualType, object instance)
    {
        ReflectionExtensions.ValidateImplementation(implementedInterface, actualType);
        ImplementedInterface = implementedInterface;
        ActualType = actualType;
        Instance = instance;
    }

    public SingletonServiceInfo(Type? implementedInterface, Type actualType)
    {
        ReflectionExtensions.ValidateImplementation(implementedInterface, actualType);
        ImplementedInterface = implementedInterface;
        ActualType = actualType;
        Dependencies = actualType.GetDependencies().ToArray();
        if (!Dependencies.Any())
        {
            Instance = Activator.CreateInstance(ActualType);
        }
    }

    


    public ServiceLifetime Lifetime => ServiceLifetime.Singleton;
    public Type? ImplementedInterface { get; }
    public Type ActualType { get; }
    public object? Instance { get; private set; }

    public IEnumerable<Type>? Dependencies { get; }

    public object ResolveDeps(object?[]? args)
    {
        Instance = Activator.CreateInstance(ActualType, args);
        return Instance;
    }
}