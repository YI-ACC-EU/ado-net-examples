namespace AirportExample.Services;

public interface IServiceModule
{
    string Name { get; }
    string Command { get; }
    void Run();
}