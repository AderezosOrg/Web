namespace backend.Services.ServicesInterfaces;

public interface ICreateSingleElement<Tin, Tout>
{
    public Task<Tout> CreateSingleElement(Tin newElement);
}
