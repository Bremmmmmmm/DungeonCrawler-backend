using System.Text.Json;
using Interface.Dtos;
using Interface.Interfaces.Dal;
using Interface.Interfaces.Logic;

namespace Logic.Containers
{
    public class MazeInteractableContainer : IMazeInteractableContainer
    {
        private readonly IItemDal _itemDal;
        private readonly HttpClient _httpClient;

        public MazeInteractableContainer(IDalFactory dalFactory)
        {
            _itemDal = dalFactory.CreateItemDal();
            _httpClient = new HttpClient();
        }

        public async Task<EnemyDto> getEnemy()
        {
            var random = new Random();
            int id = random.Next(1, 1026); // 1 to 1025 inclusive
            string url = $"https://pokeapi.co/api/v2/pokemon/{id}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var pokeData = JsonSerializer.Deserialize<PokeApiResponse>(json, options)
                           ?? throw new InvalidOperationException("Failed to parse Pokemon response.");

            if (pokeData.sprites == null)
                throw new InvalidOperationException("Pokemon sprites missing in response.");

            return new EnemyDto
            {
                id = pokeData.id,
                name = pokeData.name,
                sprite = pokeData.sprites.front_default
            };
        }

        public async Task<ItemDto> getItemInArea(int areaId)
        {
            var items = (await _itemDal.GetItemsByArea(areaId)).ToList();
            if (!items.Any())
                return null;

            var random = new Random();
            var index = random.Next(items.Count);
            var item = items[index];

            string url = $"https://pokeapi.co/api/v2/item/{item.name}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var pokeItem = JsonSerializer.Deserialize<PokeItemResponse>(json, options)
                           ?? throw new InvalidOperationException("Failed to parse Pokemon item response.");

            if (pokeItem.sprites == null)
                throw new InvalidOperationException("Item sprites missing in response.");

            item.sprite = pokeItem.sprites.@default;
            return item;
        }

        // --- Helper classes for deserialization ---
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

        private class PokeItemResponse
        {
            public string name { get; set; }
            public ItemSprites sprites { get; set; }
            public FlavorTextEntry[] flavor_text_entries { get; set; }
        }

        private class ItemSprites
        {
            public string @default { get; set; }
        }

        private class FlavorTextEntry
        {
            public string text { get; set; }
            public Language language { get; set; }
        }

        private class Language
        {
            public string name { get; set; }
        }
    }
}
