using Interface.Interfaces.Logic;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class MazeInteractableController(ILogicFactoryBuilder logicFactoryBuilder) : ControllerBase
{
    [HttpGet("GetEnemy")]
    public async Task<IActionResult> GetEnemy()
    {
        var enemy = await logicFactoryBuilder
            .BuildHandlerFactory()
            .BuildMazeInteractableHandler().getEnemy();

        return Ok(enemy);
    }

    [HttpGet("GetItemInArea")]
    public async Task<IActionResult> GetItemInArea(int areaId)
    {
        var item = await logicFactoryBuilder
            .BuildHandlerFactory()
            .BuildMazeInteractableHandler().getItemInArea(areaId);
        
        return Ok(item);
    }
}