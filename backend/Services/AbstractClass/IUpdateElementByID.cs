namespace backend.Services.AbstractClass;

public interface IUpdateElementByID<Tin, Tout>
{
    public Task<Tout> UpdateElementById(Guid elementId, Tin updateElement);
}