using Interface.Dtos;

namespace Interface.Interfaces.Dal;

public interface IItemDal
{
    Task<IEnumerable<ItemDto>> GetItemsByArea(int areaId);
}