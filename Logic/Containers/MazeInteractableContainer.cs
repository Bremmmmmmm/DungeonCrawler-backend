using System.Text.Json;
using Interface.Dtos;
using Interface.Interfaces.Dal;
using Interface.Interfaces.Logic;

namespace Logic.Containers;

public class MazeInteractableContainer(IDalFactory dalFactory) : IMazeInteractableContainer
{
    public async Task<EnemyDto> getEnemy()
    {
        var random = new Random();
        int id = random.Next(1, 1021); // 1 to 1020 inclusive
        string url = $"https://pokeapi.co/api/v2/pokemon/{id}";

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var pokeData = JsonSerializer.Deserialize<PokeApiResponse>(json);

        return new EnemyDto
        {
            name = pokeData.name,
            id = pokeData.id,
            sprite = pokeData.sprites.front_default
        };
    }

// Helper classes for deserialization
    private class PokeApiResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public Sprites sprites { get; set; }
    }

    private class Sprites
    {
        public string front_default { get; set; }
    }
}