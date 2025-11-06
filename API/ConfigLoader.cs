using System.Text.Json;
using Interface.Config;
using Interface.Shared;

namespace API;

public class ConfigLoader : IConfigLoader
{
    private readonly Config _config;

    public ConfigLoader()
    {
        // Read connection string from environment variable
        var envConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        if (string.IsNullOrEmpty(envConnectionString))
        {
            // Throw an exception if the environment variable is missing
            throw new InvalidOperationException(
                "ConnectionStrings__DefaultConnection environment variable is not set. " +
                "Please provide the database connection string via environment variables."
            );
        }

        _config = new Config
        {
            DbConfig = new DbConf
            {
                ConnectionString = envConnectionString
            }
        };
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