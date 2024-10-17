namespace backend.Services.ServicesInterfaces;

public interface IGetAllElementsService<T>
{
    public Task<List<T>> GetAllElements();
}
