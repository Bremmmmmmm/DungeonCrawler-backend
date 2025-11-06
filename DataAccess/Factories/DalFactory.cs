using DataAccess.Database;
using Interface.Config;
using Interface.Interfaces.Dal;

namespace DataAccess.Factories;

public class DalFactory(IConfigLoader configLoader) : IDalFactory
{
    public IItemDal CreateItemDal()
    {
        return new ItemDal(configLoader.GetConfig<DbConf>().ConnectionString);
    }
}