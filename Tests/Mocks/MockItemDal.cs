using Interface.Dtos;
using Interface.Interfaces.Dal;

namespace Tests.Mocks;

public class MockItemDal : IItemDal
{
    public Task<IEnumerable<ItemDto>> GetItemsByArea(int areaId)
    {
        throw new NotImplementedException();
    }
}