using Interface.Dtos;

namespace Interface.Interfaces.Logic;

public interface IMazeInteractableHandler
{
    public Task<EnemyDto> getEnemy();
    public Task<ItemDto> getItemInArea(int areaId);
}