using AirportExample.Repositories;

namespace AirportExample.Services.ServiceModules;

public class FlightServiceModule : IServiceModule
{
    private readonly IFlightsRepository _flightRepository;
    public FlightServiceModule()
    {
        _flightRepository = new FlightsRepository();
    }

    public string Name => "Gestione voli";
    public string Command => "Flights";
    public void Run()
    {
        throw new NotImplementedException();
    }
}