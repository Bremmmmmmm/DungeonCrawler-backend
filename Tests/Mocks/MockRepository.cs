using Interface.Interfaces.Dal;

namespace Tests.Mocks;

public class MockRepository : IDalFactory
{
    public IItemDal CreateItemDal()
    {
        return new MockItemDal();
    }
}