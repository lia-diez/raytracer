namespace DependencyInjection.ServiceInfo;

public class SingletonServiceInfo : IServiceInfo
{
    public SingletonServiceInfo(Type? implementedInterface, Type actualType, object instance)
    {
        Validate(implementedInterface, actualType);
        ImplementedInterface = implementedInterface;
        ActualType = actualType;
        Instance = instance;
    }

    public SingletonServiceInfo(Type? implementedInterface, Type actualType)
    {
        Validate(implementedInterface, actualType);
        ImplementedInterface = implementedInterface;
        ActualType = actualType;
        Dependencies = actualType.GetDependencies().ToArray();
    }

    private static void Validate(Type? implementedInterface, Type actualType)
    {
        if (implementedInterface == null) return;
        if (!implementedInterface.IsInterface)
            throw new Exception($"Implemented interface {implementedInterface.Name} is not an interface");
        if (!actualType.IsAssignableTo(implementedInterface))
            throw new Exception($"Type {actualType.Name} cannot be assigned to {implementedInterface.Name}");
    }


    public ServiceLifetime Lifetime => ServiceLifetime.Singleton;
    public Type? ImplementedInterface { get; }
    public Type ActualType { get; }
    public object? Instance { get; private set; }

    public IEnumerable<Type>? Dependencies { get; }

    public void ResolveDeps(object?[]? args)
    {
        Instance = Activator.CreateInstance(ActualType, args);
    }
}