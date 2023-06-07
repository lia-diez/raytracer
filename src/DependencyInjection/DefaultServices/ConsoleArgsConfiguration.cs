namespace DependencyInjection.DefaultServices;

public class ConsoleArgsConfiguration : IConfiguration
{

    private Dictionary<string, string> _dictionary;

    public ConsoleArgsConfiguration()
    {
        _dictionary = new Dictionary<string, string>();
        var args = Environment.GetCommandLineArgs();
        foreach (var arg in args)
        {
            if (!arg.StartsWith("--") || !arg.Contains('=')) continue;
            var keyAndValue = arg.Split("=", StringSplitOptions.RemoveEmptyEntries);
            if (keyAndValue.Length == 2)
            {
                _dictionary[keyAndValue[0].ToLower()] = keyAndValue[1];
            }
        }
    }
    
    public string? Get(string key)
    {

        return _dictionary.GetValueOrDefault(key);
    }

    public string? this[string key] =>_dictionary.GetValueOrDefault(key);
}