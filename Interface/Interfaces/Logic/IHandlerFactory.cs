namespace Interface.Interfaces.Logic;

public interface IHandlerFactory
{
    public IMazeHandler BuildMazeHandler();
    public IMazeInteractableHandler BuildMazeInteractableHandler();
}