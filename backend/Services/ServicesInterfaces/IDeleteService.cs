namespace backend.Services.ServicesInterfaces;

public interface IDeleteService
{
    public Task<bool> DeleteElementById(Guid elementId);
}
