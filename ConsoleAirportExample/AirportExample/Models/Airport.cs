namespace AirportExample.Models;

public class Airport
{
    public string City { get; set; }
    public string Country { get; set; }
    public int? AirstripsNumber { get; set; }

    public override string ToString()
        => $"Città:{City}, Nazione:{Country}, NumeroPiste{AirstripsNumber}";

    public string ToCommaSeparatedString() => $"{City};{Country};{AirstripsNumber}";
}