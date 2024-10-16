namespace backend.Services.AbstractClass;

public interface ICreateSingleElement<Tin, Tout>
{
    public Task<Tout> CreateSingleElement(Tin newElement);
}
