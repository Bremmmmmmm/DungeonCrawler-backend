using Interface.Interfaces.Dal;
using Interface.Interfaces.Logic;

namespace Logic.Factories;

public class LogicFactoryBuilder(IDalFactory dalFactory) : ILogicFactoryBuilder
{
    public IHandlerFactory BuildHandlerFactory()
    {
        return new HandlerFactory(BuildContainerFactory());
    }

    public IContainerFactory BuildContainerFactory()
    {
        return new ContainerFactory(dalFactory);
    }
}