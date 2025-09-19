using Interface.Dtos;
using Interface.Interfaces.Logic;

namespace Logic.Handlers;

public class MazeInteractableHandler(IMazeInteractableContainer mazeInteractableContainer) : IMazeInteractableHandler
{
    public async Task<EnemyDto> getEnemy()
    {
        return await mazeInteractableContainer.getEnemy();
    }
}