using System.Reflection;

namespace DependencyInjection.ServiceInfo;

public static class ReflectionExtensions
{
    public static IEnumerable<Type> GetDependencies(this Type t)
    {
        return t.GetConstructors().First().GetParameters().Select(c => c.ParameterType);
    }
}