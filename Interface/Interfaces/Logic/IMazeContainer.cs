using Interface.Dtos;

namespace Interface.Interfaces.Logic;

public interface IMazeContainer
{
    public Task<MazeDto> CreateMaze(int size);
}