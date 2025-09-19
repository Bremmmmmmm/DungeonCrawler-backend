using Interface.Interfaces.Logic;
using Logic.Handlers;

namespace Logic.Factories;

public class HandlerFactory(IContainerFactory containerFactory) : IHandlerFactory
{
    public IMazeHandler BuildMazeHandler()
    {
        return new MazeHandler(containerFactory.BuildMazeContainer());
    }

    public IMazeInteractableHandler BuildMazeInteractableHandler()
    {
        return new MazeInteractableHandler(containerFactory.BuildMazeInteractableContainer());
    }
}