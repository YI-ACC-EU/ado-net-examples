using Euris.Examples.Business.Extensions;
using Euris.Examples.Common.Models.Dto;
using Euris.Examples.Common.Models.Requests;
using Euris.Examples.Common.Models.Responses;
using Euris.Examples.Common.Repositories;
using Euris.Examples.Common.Services;
using Serilog;

namespace Euris.Examples.Business;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly ILogger _logger;
    public MovieService(
        ILogger logger,
        IMovieRepository movieRepository)
    {
        _logger = logger;
        _movieRepository = movieRepository;
    }

    public async Task<DefaultResponse<DefaultMovieResponseDtoCommon>> GetMovieById(MovieByIdRequest? request)
    {
        var response = new DefaultResponse<DefaultMovieResponseDtoCommon>()
        {
            StatusCode = 400
        };

        if (request?.Id is null)
        {
            // meglio validare altrove, per esempio definire un decorator.
            response.Errors = new[] {"Invalid parameter: Id"};
            return response;
        }
        var movieId = request.Id;
        try
        {
            var model = await _movieRepository.GetMovieById(movieId);
            if (model != null)
            {
                model.Crew = await _movieRepository.GetCrewByMovieId(movieId);
                model.Actors = await _movieRepository.GetActorByMovieId(movieId);
                model.Companies = await _movieRepository.GetCompaniesByMovieId(movieId);
                model.Genres = await _movieRepository.GetGenresByMovieId(movieId);
                model.ProductionCountries = await _movieRepository.GetCountriesByMovieId(movieId);
                response = model.ToResponse();
            }
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error fetching model.");
            response.StatusCode = 500;
            response.Errors = new[] {e.Message};

        }
        return response;
    }

    public async Task<DefaultResponse<MovieSearchResponseDto>> GetMoviesListByFilter(MoviesByFilterRequest request)
    {
        var response = new DefaultResponse<MovieSearchResponseDto>()
        {
            StatusCode = 400
        };
        try
        {
           var result = await _movieRepository.GetMoviesByFilter(request.ToMoviesByFilterRequest());
           response.StatusCode = 200;
           response.Data = result.ToResponseDto();
        }
        catch (Exception e)
        {
            _logger.Error(e, "Can't get filtered movies");
            response.StatusCode = 500;
        }

        return response;
    }
}