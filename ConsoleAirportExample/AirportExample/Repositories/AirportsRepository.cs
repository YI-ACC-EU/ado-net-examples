using System.Data;
using AirportExample.Models;
using Microsoft.Data.SqlClient;
using static AirportExample.Constants;
namespace AirportExample.Repositories;

public interface IAirportRepository
{
    Airport? GetById (string airportId);
    Airport? GetByNation (string nation);
    Airport? Insert (Airport airport);
    Airport? Update (Airport airport);
    bool Delete (string airportId);
}

public class AirportsRepository : IAirportRepository
{
    public Airport? GetById(string airportId)
    {
        var command = "SELECT * FROM Aeroporto WHERE Citta = @Citta";
        return GetAirport(command, "@Citta", airportId);
    }

    public Airport? GetByNation(string nation)
    {
        var command = "SELECT * FROM Aeroporto WHERE Nazione = @Nazione";
        return GetAirport(command, "@Nazione", nation);
    }

    private Airport? GetAirport(string command, string parameterName, string value)
    {
        try
        {
            using var cn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(command, cn);
            cn.Open();
            cmd.Parameters.AddWithValue(parameterName, value);
            using var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleRow);
            if (reader?.Read() == true)
            {
                return new Airport()
                {
                    City = reader.GetString("Citta"),
                    Country = reader.GetString("Nazione"),
                    AirstripsNumber = reader.GetInt32("NumPiste")
                };
            }
        }
        catch (SqlException ex)
        {
            Console.Error.WriteLine(ex);
        }
        return null;
    }


    public Airport? Insert(Airport airport)
    {
        var command = @"
            INSERT INTO [Aeroporto] ([Citta],[Nazione],[NumPiste])
            VALUES ( @Citta, @Nazione,@NumPiste )";
        var p = new Dictionary<string, object >()
        {
            { "@Citta", airport.City },
            { "@Nazione", airport.Country},
            { "@NumPiste", airport.AirstripsNumber?? 0}
        };
        var rowsAffected = Execute(command, p);
        return rowsAffected > 0 
            ? GetById(airport.City) 
            : null;
    }

    public Airport? Update(Airport airport)
    {
        var command = @"
            UPDATE [Aeroporto]
            SET 
            [Nazione] = @Nazione,
            [NumPiste] = @NumPiste
            WHERE [Citta] = @Citta;";
        var p = new Dictionary<string, object >()
        {
            { "@Citta", airport.City },
            { "@Nazione", airport.Country},
            { "@NumPiste", airport.AirstripsNumber?? 0}
        };
        var rowsAffected = Execute(command, p);
        return rowsAffected > 0 
            ? GetById(airport.City) 
            : null;
    }

    public bool Delete(string airportId)
    {
        var command = @"DELETE FROM [Aeroporto] WHERE [Citta] = @Citta;";
        var p = new Dictionary<string, object >()
        {
            { "@Citta", airportId }
        };
        var rowsAffected = Execute(command, p);
        return rowsAffected.HasValue;
    }

    private int? Execute(string command, Dictionary<string, object> parameters)
    {
        try
        {
            using var cn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(command, cn);
            cn.Open();  
            var p = parameters.Select(x => new SqlParameter(x.Key, x.Value)).ToArray();
            cmd.Parameters.AddRange(p);
            return cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            Console.Error.WriteLine(ex);
        }
        return null;
    }
}