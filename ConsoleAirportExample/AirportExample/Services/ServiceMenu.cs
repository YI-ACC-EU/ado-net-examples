using AirportExample.Services.ServiceModules;
using static AirportExample.Constants.CrudOperations;
namespace AirportExample.Services;

public class ServiceMenu 
{
    
    
    public void Start()
    {
        Console.WriteLine("Airport Example");
        var serviceModules = GetServiceModules();
        var choice = default(string);
        do
        {
            Console.WriteLine("START MENU");
            Console.WriteLine("Digitare: ");

            foreach (var sm in serviceModules)
                Console.WriteLine($"[{sm.Command}]:{sm.Name}");

            Console.WriteLine("[X]:Uscire"); 
            choice = Console.ReadLine();
            var service = serviceModules
                .FirstOrDefault(x=>
                    x.Command
                        .Equals(choice, StringComparison.InvariantCultureIgnoreCase));

            if(service is null) 
                Console.WriteLine($"Commando non valido:{choice}");

            service?.Run();
        } while (choice?.Equals(ExitChoice, StringComparison.InvariantCulture) != true);
    }

    private List<IServiceModule> GetServiceModules()
        => new ()
        {
            new AirportServiceModule(),
            new FlightServiceModule(),
            new PlaneServiceModule(),
        };
}

