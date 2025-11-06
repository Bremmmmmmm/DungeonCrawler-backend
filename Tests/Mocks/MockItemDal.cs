using Interface.Dtos;
using Interface.Interfaces.Dal;

namespace Tests.Mocks;

public class MockItemDal : IItemDal
{
    public IEnumerable<ItemDto> _itemDtos = new List<ItemDto>
    {
        new()
        {
            name = "poke-ball",
            displayName = "Item 1",
            areaNumbers = new[] { 1 },
            dropChance = 70
        },
        new()
        {
            name = "poke-ball",
            displayName = "Item 2",
            areaNumbers = new[] { 2 },
            dropChance = 70
        },
    };

    public Task<IEnumerable<ItemDto>> GetItemsByArea(int areaId)
    {
        var items = _itemDtos.Where(i => i.areaNumbers.Contains(areaId));
        return Task.FromResult(items);
    }
}