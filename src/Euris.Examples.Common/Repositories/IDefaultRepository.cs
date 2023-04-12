using Euris.Examples.Common.Models.Entities;

namespace Euris.Examples.Common.Repositories;

public interface IMovieRepository
{
    Task<Movie?> GetMovieById(int id);
    Task<List<Actor>> GetActorByMovieId(int movieId);
    Task<List<CrewMember>> GetCrewByMovieId(int movieId);
    Task<List<Company>> GetCompaniesByMovieId(int movieId);
    Task<List<Country>> GetCountriesByMovieId(int movieId);
    Task<List<Genre>> GetGenresByMovieId(int movieId);
}