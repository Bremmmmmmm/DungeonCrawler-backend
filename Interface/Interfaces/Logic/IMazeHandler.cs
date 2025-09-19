using Interface.Dtos;

namespace Interface.Interfaces.Logic;

public interface IMazeHandler
{
    public Task<MazeDto> CreateMaze(int size);
}