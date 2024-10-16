namespace backend.Services.ServicesInterfaces;

public interface IGetElementById<T>
{
    public Task<T> GetElementById(Guid elementId);
}
