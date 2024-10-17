namespace backend.Services.ServicesInterfaces;

public interface IUpdateElementByID<Tin, Tout>
{
    public Task<Tout> UpdateElementById(Guid elementId, Tin updateElement);
}
