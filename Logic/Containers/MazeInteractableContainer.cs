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
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public MazeInteractableContainer(IDalFactory dalFactory)
        {
            _itemDal = dalFactory.CreateItemDal();
            _httpClient = new HttpClient();
        }

        public async Task<EnemyDto> getEnemy()
        {
            var random = new Random();
            int id = random.Next(1, 1026);
            var url = $"https://pokeapi.co/api/v2/pokemon/{id}";

            var pokeData = await FetchJsonAsync<PokeApiResponse>(url);
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
            var item = items[random.Next(items.Count)];

            var url = $"https://pokeapi.co/api/v2/item/{item.name}";
            var pokeItem = await FetchJsonAsync<PokeItemResponse>(url);
            if (pokeItem.sprites == null)
                throw new InvalidOperationException("Item sprites missing in response.");

            item.sprite = pokeItem.sprites.@default;
            return item;
        }

        private async Task<T> FetchJsonAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, _jsonOptions)
                   ?? throw new InvalidOperationException("Failed to parse response.");
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
