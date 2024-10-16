namespace backend.Services.AbstractClass;

public interface IGetElementById<T>
{
    public Task<T> GetElementById(Guid elementId);
}
