namespace DependencyInjection;

public static class ReflectionExtensions
{
    public static IEnumerable<Type> GetDependencies(this Type t)
    {
        return t.GetConstructors().First().GetParameters().Select(c => c.ParameterType);
    }
    
    public static void ValidateImplementation(Type? implementedInterface, Type actualType)
    {
        if (implementedInterface == null) return;
        if (!implementedInterface.IsInterface)
            throw new Exception($"Implemented interface {implementedInterface.Name} is not an interface");
        if (!actualType.IsAssignableTo(implementedInterface))
            throw new Exception($"Type {actualType.Name} cannot be assigned to {implementedInterface.Name}");
    }
}