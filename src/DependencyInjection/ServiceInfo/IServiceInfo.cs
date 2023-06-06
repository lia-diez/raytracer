namespace DependencyInjection.ServiceInfo;

public interface IServiceInfo
{
    public ServiceLifetime Lifetime { get; }
    public Type? ImplementedInterface { get; }
    public Type ActualType { get; }
    public object? Instance { get; }

    public IEnumerable<Type>? Dependencies { get;}

    public object ResolveDeps(object?[]? args);
}