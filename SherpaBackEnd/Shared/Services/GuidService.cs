namespace SherpaBackEnd.Shared.Services;

public class GuidService : IGuidService
{
    public Guid GenerateRandomGuid()
    {
        return Guid.NewGuid();
    }
}