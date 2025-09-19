using System.Text.Json;

namespace Interface.Shared;

public class JsonOptionData
{
    public static JsonSerializerOptions Default { get; } = new()
    {
        PropertyNameCaseInsensitive = true
    };
}