namespace DependencyInjection.DefaultServices;

public interface IConfiguration
{
    public string? Get(string key);
    public string? this[string key] { get; }
}