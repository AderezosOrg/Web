namespace Db;

public interface IDAO <T>
{
    int Create(T element);
    T? Read(Guid id);
    List<T> ReadAll();
    int Update(T element);
    bool Delete(Guid id);
}