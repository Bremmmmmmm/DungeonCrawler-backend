using Interface.Interfaces.Logic;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class MazeController(ILogicFactoryBuilder logicFactoryBuilder) : ControllerBase
{
    [HttpGet("GetMaze")]
    public async Task<IActionResult> GetMaze(int size = 7)
    {
        var maze = await logicFactoryBuilder
            .BuildHandlerFactory()
            .BuildMazeHandler()
            .CreateMaze(size);

        return Ok(maze);
    }
}