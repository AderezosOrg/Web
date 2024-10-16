namespace backend.Services.AbstractClass;

public interface IGetAllElementsService<T>
{
    public Task<List<T>> GetAllElements();
}