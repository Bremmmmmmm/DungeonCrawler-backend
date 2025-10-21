using Interface.Interfaces.Logic;
using Logic.Factories;
using Logic.Handlers;
using Moq;
using MockRepository = Tests.Mocks.MockRepository;

namespace Tests.Handlers;

[TestClass]
public class MazeHandlerTests
{
    private IMazeContainer _MazeContainer = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        var dalFactory = new MockRepository();
        _MazeContainer = new LogicFactoryBuilder(dalFactory).BuildContainerFactory().BuildMazeContainer();
    }

    [TestMethod]
    public void MazeHandler_Constructor_Test()
    {
        // Act
        var mazeHandler = new MazeHandler(_MazeContainer);
        // Assert
        Assert.IsNotNull(mazeHandler);
    }

    [TestMethod]
    public async Task MazeHandler_CreateMaze_Test()
    {
        // Arrange
        var mazeHandler = new MazeHandler(_MazeContainer);
        int size = 5;
        // Act
        var mazeDto = await mazeHandler.CreateMaze(size);
        // Assert
        Assert.IsNotNull(mazeDto);
        Assert.AreEqual(size, mazeDto.size);
    }

}