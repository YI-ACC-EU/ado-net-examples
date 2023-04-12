using System.Data;
using Euris.Examples.Common.Models.Entities;
using Euris.Examples.Common.Repositories;
using Euris.Examples.Data.Extensions;
using Microsoft.Data.SqlClient;
using static Euris.Examples.Data.MovieQueries;

namespace Euris.Examples.Data;

public class MovieRepository : IMovieRepository
{
    private readonly IDatabaseOptions _dbOptions;
    public MovieRepository(IDatabaseOptions options)
    {
        _dbOptions = options;
    }

    public async Task<Movie?> GetMovieById(int id)
    {
        await using var connection = new SqlConnection(_dbOptions.ConnectionString);
        await using var command = new SqlCommand(GetMovieQuery(), connection);
        command.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) {Value = id});
        connection.Open();
        await using var reader = await command.ExecuteReaderAsync(
            CommandBehavior.SingleRow | CommandBehavior.CloseConnection);
        return reader?.Read() == true 
            ? reader.MapToMovie()
            : default;
    }

    public Task<List<Actor>> GetActorByMovieId(int movieId)
        => ExecuteReader(movieId, GetActorsByMovieIdQuery(), reader =>
        {
            List<Actor> result = new();
            while (reader?.Read()==true)
                result.Add(reader.MapToActor());
            return result;
        });

    public Task<List<CrewMember>> GetCrewByMovieId(int movieId)
        => ExecuteReader(movieId, GetCrewByMovieIdQuery(), reader =>
        {
            List<CrewMember> result = new();
            while (reader?.Read()==true)
                result.Add(reader.MapToCrewMember());
            return result;
        });

    public Task<List<Company>> GetCompaniesByMovieId(int movieId)
        => ExecuteReader(movieId, GetCompanyByMovieIdQuery(), reader =>
        {
            List<Company> result = new();
            while (reader?.Read()==true)
                result.Add(reader.MapToCompany());
            return result;
        });

    public Task<List<Country>> GetCountriesByMovieId(int movieId)
        => ExecuteReader(movieId, GetCountryByMovieIdQuery(), reader =>
        {
            List<Country> result = new();
            while (reader?.Read()==true)
                result.Add(reader.MapToCountry());
            return result;
        });

    public Task<List<Genre>> GetGenresByMovieId(int movieId)
        => ExecuteReader(movieId, GetGenreByMovieIdQuery(), reader =>
        {
            List<Genre> result = new();
            while (reader?.Read()==true)
                result.Add(reader.MapToGenre());
            return result;
        });

    private async Task<List<T>> ExecuteReader<T>(
        int movieId, 
        string commandText, 
        Func<IDataReader?, List<T>> func)
    {
        await using var connection = new SqlConnection(_dbOptions.ConnectionString);
        await using var command = new SqlCommand(commandText, connection);
        command.Parameters.Add(new SqlParameter("MovieId", SqlDbType.Int) {Value = movieId});
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        var result = func(reader);
        return result;
    }
}