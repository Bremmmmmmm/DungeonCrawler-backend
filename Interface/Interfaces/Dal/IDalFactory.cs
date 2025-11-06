namespace Interface.Interfaces.Dal;

public interface IDalFactory
{
    public IItemDal CreateItemDal();
}