using Interface.Interfaces.Logic;
using Logic.Factories;
using Tests.Mocks;

namespace Tests.Factories;

[TestClass]
public class HandlerFactoryTests
{
    private IContainerFactory _containerFactory = null!;
    
    [TestInitialize]
    public void Initialize()
    {
        _containerFactory = new ContainerFactory(new MockRepository());
    }
    
    [TestMethod]
    public void ConstructorTest()
    {
        //arrange
        //act
        var handlerFactory = new HandlerFactory(_containerFactory);
        //assert
        Assert.IsNotNull(handlerFactory);
    }
    
    [TestMethod]
    public void BuildMazeHandlerTest()
    {
        //arrange
        var handlerFactory = new HandlerFactory(_containerFactory);
        //act
        var mazeHandler = handlerFactory.BuildMazeHandler();
        //assert
        Assert.IsNotNull(mazeHandler);
    }
    
    [TestMethod]
    public void BuildMazeInteractableHandlerTest()
    {
        //arrange
        var handlerFactory = new HandlerFactory(_containerFactory);
        //act
        var mazeInteractableHandler = handlerFactory.BuildMazeInteractableHandler();
        //assert
        Assert.IsNotNull(mazeInteractableHandler);
    }
}