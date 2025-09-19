namespace Interface.Config;

public interface IConfigLoader
{
    public T GetConfig<T>();
}