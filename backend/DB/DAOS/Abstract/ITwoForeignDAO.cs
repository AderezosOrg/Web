namespace Db;

public interface ITwoForeignDAO <T>
{
    int Create(T element);
    T? Read(Guid id1, Guid id2);
    List<T> ReadAll();
    int Update(T element);
    bool Delete(Guid id1, Guid id2);
}