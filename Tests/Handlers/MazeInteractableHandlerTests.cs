using Interface.Interfaces.Logic;
using Interface.Dtos;
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
        var mazeInteractableHandler = new MazeInteractableHandler(_mazeInteractableContainer);
        Assert.IsNotNull(mazeInteractableHandler);
    }

    [TestMethod]
    public async Task MazeInteractableHandler_getEnemy_Test()
    {
        var mazeInteractableHandler = new MazeInteractableHandler(_mazeInteractableContainer);
        var enemy = await mazeInteractableHandler.getEnemy();
        Assert.IsNotNull(enemy);
        Assert.IsInstanceOfType(enemy, typeof(EnemyDto));
    }

    [TestMethod]
    public async Task MazeInteractableHandler_getItemInArea_Test()
    {
        var mazeInteractableHandler = new MazeInteractableHandler(_mazeInteractableContainer);
        var item = await mazeInteractableHandler.getItemInArea(1);

        // item can be null if no items exist for the area in the mock repository.
        if (item != null)
        {
            Assert.IsInstanceOfType(item, typeof(ItemDto));
            Assert.IsFalse(string.IsNullOrEmpty(item.name));
        }
        else
        {
            Assert.IsNull(item);
        }
    }
}