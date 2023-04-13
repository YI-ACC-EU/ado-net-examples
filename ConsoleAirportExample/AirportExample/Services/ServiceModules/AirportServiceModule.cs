using AirportExample.Models;
using AirportExample.Repositories;
using static AirportExample.Constants.CrudOperations;
namespace AirportExample.Services.ServiceModules;

public class AirportServiceModule : IServiceModule
{
    private readonly IAirportRepository _airportRepository;
    public AirportServiceModule()
    {
        _airportRepository = new AirportsRepository();
    }
    public string Name => "Gestione aeroporti";
    public string Command => "Airport";

    private const string SearchByName = "SNM";
    private const string SearchByNation = "SNA";

    public void Run()
    {
        var choice = default(string);
        do
        {
            Console.WriteLine("Gestione aeroporti");
            var operations = Operations();
            operations.Add(SearchByName, "Cerca per nome");
            operations.Add(SearchByNation, "Cerca per nazione");
            foreach (var operation in operations)
                Console.WriteLine($"[{operation.Key}] : {operation.Value}");
            choice = Console.ReadLine() ?? string.Empty;
            ManageChoice(choice);
        } while (ExitChoice.Equals(choice));
    }

    private void ManageChoice(string choice)
    {
        switch (choice)
        {
            case Create: CreateAirport(); break;
            case Update: UpdateAirport(); break;
            case Delete: DeleteAirport(); break;
            case SearchByName : SearchAirportByName(); break;
            case SearchByNation : SearchAirportByNation(); break;
        }
    }

    private void CreateAirport()
    {
        Console.WriteLine("Inserire il nome della citta nazione e numero di piste separati da \";\":");
        Console.WriteLine("Per esempio: \"Napoli;Italia;20\"");
        Console.WriteLine("Oppure \"Napoli;Italia;\" se si vuole ommettere numero di piste");
        var items = Console.ReadLine();
        var airport = Parse(items);
        
        var existingAirport = Search(false, airport?.City, out var searchErrors);
        if (existingAirport != null)
        {
            Console.WriteLine("Aeroporto esiste già. Operazione annullata");
            return;
        }

        if (searchErrors)
        {
            Console.WriteLine("Non posso verificare l'esistenza dell'aeroporto. Operazione annullata");
            return;
        }

        try
        {
           var savedAirport =  _airportRepository.Insert(airport);
           var message = savedAirport == null
               ? "Salvataggio non riuscito"
               : "Salvataggio completato";
           Console.WriteLine(message);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"{ex.Message} {ex.StackTrace}");
        }
    }

    private void UpdateAirport()
    {
        var cityName = Prompt("Inserire il nome della città:");
        if (cityName == null) return;
        try
        {
            var existingAirport = Search(false, cityName, out var searchErrors);
            if (existingAirport == null)
            {
                Console.WriteLine("Aeroporto non esiste. Operazione annullata");
                return;
            }
            var values =
                Prompt($"Copiare, modificare e re-inserire la stringa: \"{existingAirport.ToCommaSeparatedString()}\"");

            var modifiedAirport = Parse(values);
            if (!existingAirport.City.Equals(modifiedAirport?.City))
            {
                Console.WriteLine("Nome della città non modificabile");
                return;
            }

            var savedAirport = _airportRepository.Update(modifiedAirport);
            var message = savedAirport == null
                ? "Salvataggio non riuscito"
                : "Salvataggio completato";
            Console.WriteLine(message);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"{ex.Message} {ex.StackTrace}");
        }
    }

    private void DeleteAirport()
    {
        var cityName = Prompt("Inserire il nome della città:");
        try
        {
           var result =  _airportRepository.Delete(cityName);
           var message = result
               ? "Salvataggio completato"
               : "Salvataggio non riuscito";
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"{ex.Message} {ex.StackTrace}");
        }
    }

    private void SearchAirportByName()
    {
        var cityName = Prompt("Inserire il nome della citta:");
        Search(false, cityName, out _);
    }

    private void SearchAirportByNation()
    {
        var countryName = Prompt("Inserire il nome della nazione:");
        Search(true, countryName, out _);
    }

    private Airport? Search(bool byNation, string searchString, out bool hasErrors)
    {
        try
        {
            hasErrors = false;
            return byNation 
                ? _airportRepository.GetByNation(searchString)
                : _airportRepository.GetById(searchString);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"{ex.Message} {ex.StackTrace}");
            hasErrors = true;
            return null;
        }
    }

    private string? Prompt(string text)
    {
        Console.WriteLine(text);
        var cityName = Console.ReadLine();
        if (cityName != null) 
            return cityName;
        Console.WriteLine("Valore inserito non può essere vuoto.");
        return null;
    }

    private Airport? Parse(string? text)
    {
        var splitted = text?.Split(';') ?? Array.Empty<string>();
        if (splitted.Length < 2 || splitted.Length > 3)
        {
            Console.WriteLine("Impossibile cnvertire i valori della stringa in aeroporto");
            return null;
        }
        var city = splitted[0];
        var country = splitted[1];
        var stripsNum = splitted.Length == 3
            ? int.TryParse(splitted[2], out var strips)
                ? strips
                : default
            : default;
        return new Airport()
        {
            City = city,
            Country = country,
            AirstripsNumber = stripsNum
        };
    }
}