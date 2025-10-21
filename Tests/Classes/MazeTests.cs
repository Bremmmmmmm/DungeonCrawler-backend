using Logic.Classes;
using Interface.Dtos;

namespace Tests.Classes;

[TestClass]
public class MazeTests
{
    private MazeDto _mazeDto = null!;

    [TestInitialize]
    public void Setup()
    {
        _mazeDto = new MazeDto
        {
            size = 2,
            hConnections = new bool[][]
            {
                new bool[] { true, false },
                new bool[] { false, true }
            },
            vConnections = new bool[][]
            {
                new bool[] { false, true },
                new bool[] { true, false }
            },
            roomValues = new string[][]
            {
                new string[] { "A", "B" },
                new string[] { "C", "D" }
            }
        };
    }

    [TestMethod]
    public void MazeDto_Properties_ShouldBeSetCorrectly()
    {
        var maze = new Maze(_mazeDto);
        Assert.AreEqual(_mazeDto.size, maze.size);
    }

    [TestMethod]
    public void Maze_HConnections_ShouldMatchDto()
    {
        var maze = new Maze(_mazeDto);
        for (int i = 0; i < _mazeDto.size; i++)
        {
            for (int j = 0; j < _mazeDto.size; j++)
            {
                Assert.AreEqual(_mazeDto.hConnections[i][j], maze.hConnections[i, j]);
            }
        }
    }

    [TestMethod]
    public void Maze_VConnections_ShouldMatchDto()
    {
        var maze = new Maze(_mazeDto);
        for (int i = 0; i < _mazeDto.size; i++)
        {
            for (int j = 0; j < _mazeDto.size; j++)
            {
                Assert.AreEqual(_mazeDto.vConnections[i][j], maze.vConnections[i, j]);
            }
        }
    }

    [TestMethod]
    public void Maze_RoomValues_ShouldMatchDto()
    {
        var maze = new Maze(_mazeDto);
        for (int i = 0; i < _mazeDto.size; i++)
        {
            for (int j = 0; j < _mazeDto.size; j++)
            {
                Assert.AreEqual(_mazeDto.roomValues[i][j], maze.roomValues[i, j]);
            }
        }
    }

    [TestMethod]
    public void Maze_SizeConstructor_ShouldInitializeArraysCorrectly()
    {
        int size = 3;
        var maze = new Maze(size);

        Assert.AreEqual(size, maze.size);
        Assert.AreEqual(size, maze.visited.GetLength(0));
        Assert.AreEqual(size, maze.visited.GetLength(1));
        Assert.AreEqual(size, maze.hConnections.GetLength(0));
        Assert.AreEqual(size - 1, maze.hConnections.GetLength(1));
        Assert.AreEqual(size - 1, maze.vConnections.GetLength(0));
        Assert.AreEqual(size, maze.vConnections.GetLength(1));
        Assert.AreEqual(size, maze.roomValues.GetLength(0));
        Assert.AreEqual(size, maze.roomValues.GetLength(1));
    }

    [TestMethod]
    public void Maze_ToDto_ShouldReturnEquivalentDto()
    {
        var maze = new Maze(_mazeDto);
        var dto = maze.ToDto();

        Assert.AreEqual(_mazeDto.size, dto.size);

        for (int i = 0; i < _mazeDto.size; i++)
        {
            for (int j = 0; j < _mazeDto.size; j++)
            {
                Assert.AreEqual(_mazeDto.hConnections[i][j], dto.hConnections[i][j]);
                Assert.AreEqual(_mazeDto.vConnections[i][j], dto.vConnections[i][j]);
                Assert.AreEqual(_mazeDto.roomValues[i][j], dto.roomValues[i][j]);
            }
        }
    }
}
