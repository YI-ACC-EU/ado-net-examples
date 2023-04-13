using AirportExample.Repositories;

namespace AirportExample.Services.ServiceModules;

public class PlaneServiceModule : IServiceModule
{
    private readonly IPlanesRepository _planesRepository;
    public PlaneServiceModule()
    {
        _planesRepository = new PlanesRepository();
    }
    public string Name => "Gestione aereo";
    public string Command => "Plane";
    public void Run()
    {
        throw new NotImplementedException();
    }
}