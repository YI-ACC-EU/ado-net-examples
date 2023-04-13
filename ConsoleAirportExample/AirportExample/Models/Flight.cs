namespace AirportExample.Models;

public class Flight
{
    public string Id { get; set; }
    public string WeekDay { get; set; }
    public Airport Departure { get; set; }
    public Airport Arrival { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public TimeSpan ArrivalTime { get;}
    public Plane Plane { get; set; }
}