using Interface.Interfaces.Logic;
using Logic.Factories;
using Logic.Handlers;
using Tests.Mocks;

namespace Tests.Handlers;

[TestClass]
public class MazeInteractableHandlerTests
{
    private IMazeInteractableContainer _mazeInteractableContainer = null!;

    [TestInitialize]
    public void Initialize()
    {
        var dalFactory = new MockRepository();
        _mazeInteractableContainer = new LogicFactoryBuilder(dalFactory).BuildContainerFactory()
            .BuildMazeInteractableContainer();
    }

    [TestMethod]
    public void MazeInteractableHandler_Constructor_Test()
    {
        // Arrange
        var mazeInteractableHandler = new MazeInteractableHandler(_mazeInteractableContainer);
        // Assert
        Assert.IsNotNull(mazeInteractableHandler);
    }

    [TestMethod]
    public void MazeInteractableHandler_getEnemy_Test()
    {
        // Arrange
        var mazeInteractableHandler = new MazeInteractableHandler(_mazeInteractableContainer);
        // Act
        var enemy = mazeInteractableHandler.getEnemy();
        // Assert
        Assert.IsNotNull(enemy);
    }
}