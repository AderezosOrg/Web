namespace backend.Services.AbstractClass;

public interface IDeleteService
{
    public Task<bool> DeleteElementById(Guid elementId);
}
