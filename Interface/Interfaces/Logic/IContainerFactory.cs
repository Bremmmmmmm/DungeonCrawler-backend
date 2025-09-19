namespace Interface.Interfaces.Logic;

public interface IContainerFactory
{
    public IMazeContainer BuildMazeContainer();
    public IMazeInteractableContainer BuildMazeInteractableContainer();
}