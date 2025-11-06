using System.Text.Json;
using Interface.Config;
using Interface.Shared;

namespace API;

public class ConfigLoader : IConfigLoader
{
    private readonly Config _config;

    public ConfigLoader()
    {
        // Load JSON config
        var jsonConfig = JsonSerializer.Deserialize<Config>(
            File.ReadAllText("config.json"),
            JsonOptionData.Default
        );

        // Check for environment variable override
        var envConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        if (!string.IsNullOrEmpty(envConnectionString))
        {
            jsonConfig!.DbConfig.ConnectionString = envConnectionString;
        }

        _config = jsonConfig!;
    }

    public T GetConfig<T>()
    {
        return typeof(T) switch
        {
            _ when typeof(T) == typeof(Config) => (T)(object)_config,
            _ when typeof(T) == typeof(DbConf) => (T)(object)_config.DbConfig,
            _ => throw new Exception("Config type not found")
        };
    }
}