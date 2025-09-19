using Interface.Interfaces.Dal;
using Interface.Interfaces.Logic;
using Logic.Containers;

namespace Logic.Factories;

public class ContainerFactory(IDalFactory dalFactory) : IContainerFactory
{
    public IMazeContainer BuildMazeContainer()
    {
        return new MazeContainer(dalFactory);
    }

    public IMazeInteractableContainer BuildMazeInteractableContainer()
    {
        return new MazeInteractableContainer(dalFactory);
    }
}