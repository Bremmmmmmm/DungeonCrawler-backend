using System.Text.Json;
using Interface.Config;
using Interface.Shared;

namespace API;

public class ConfigLoader : IConfigLoader
{
    private readonly Config _config = JsonSerializer.Deserialize<Config>(File.ReadAllText("config.json"), JsonOptionData.Default);

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