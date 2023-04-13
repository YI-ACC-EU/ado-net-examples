using AirportExample.Models;
using static AirportExample.Constants;

namespace AirportExample.Repositories;

public interface IFlightsRepository
{
     Flight GetById (string flightId);
     Flight Insert (Flight flight);
     Flight Update (Flight flight);
     Flight Delete (string flightId);
}
public class FlightsRepository : IFlightsRepository
{

    public Flight GetById(string flightId)
    {
        throw new NotImplementedException();
    }

    public Flight Insert(Flight flight)
    {
        throw new NotImplementedException();
    }

    public Flight Update(Flight flight)
    {
        throw new NotImplementedException();
    }

    public Flight Delete(string flightId)
    {
        throw new NotImplementedException();
    }
}