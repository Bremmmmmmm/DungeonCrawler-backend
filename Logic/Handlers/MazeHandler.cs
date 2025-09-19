using Interface.Dtos;
using Interface.Interfaces.Logic;

namespace Logic.Handlers;

public class MazeHandler(IMazeContainer mazeContainer) : IMazeHandler
{
    public async Task<MazeDto> CreateMaze(int size)
    {
        return await mazeContainer.CreateMaze(size);
    }
}