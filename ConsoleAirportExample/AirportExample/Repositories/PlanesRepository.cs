using AirportExample.Models;
using static AirportExample.Constants;

namespace AirportExample.Repositories;

public interface IPlanesRepository
{
    Plane GetById (string planeId);
    Plane Insert (Plane plane);
    Plane Update (Plane plane);
    Plane Delete (string planeId);
}

public class PlanesRepository : IPlanesRepository
{
    public Plane GetById(string planeId)
    {
        throw new NotImplementedException();
    }

    public Plane Insert(Plane plane)
    {
        throw new NotImplementedException();
    }

    public Plane Update(Plane plane)
    {
        throw new NotImplementedException();
    }

    public Plane Delete(string planeId)
    {
        throw new NotImplementedException();
    }
}