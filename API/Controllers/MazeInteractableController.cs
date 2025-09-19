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
}