using Euris.Examples.Common.Models.Dto;
using Euris.Examples.Common.Models.Entities;
using Euris.Examples.Common.Models.Requests;
using Euris.Examples.Common.Models.Responses;

namespace Euris.Examples.Business.Extensions;

public static class DefaultModelExtensions
{
    public static DefaultResponse<DefaultMovieResponseDtoCommon> ToResponse(this Movie? model)
        => model is null
            ? new DefaultResponse<DefaultMovieResponseDtoCommon>()
                {
                    StatusCode = 404,
                    Errors = new[] {"Not found"}
                }
            : new DefaultResponse<DefaultMovieResponseDtoCommon>()
                {
                    StatusCode = 200,
                    Data =  new DefaultMovieResponseDtoCommon()
                    {
                        VoteCount = model.VoteCount,
                        Revenue = model.Revenue,
                        Popularity = model.Popularity,
                        ReleaseDate = model.ReleaseDate,
                        Budget = model.Budget,
                        VoteAverage = model.VoteAverage,
                        HomePage = model.HomePage,
                        Overview = model.Overview,
                        Status = model.Status,
                        RunTime = model.RunTime,
                        Tagline = model.Tagline,
                        Title = model.Name,
                        Actors = model.Actors?.Select(x=>new ActorDto
                        {
                            Name = x.Name,
                            Gender = x.Gender,
                            Character = x.CharacterName

                        }).ToList(),
                        Companies =  model.Companies?.Select(x=>x.Name).ToList(),
                        Crew = model.Crew?.Select(x=>new CrewMemberDto()
                        {
                            Name = x.Name,
                            Job = x.Job
                        }).ToList(),
                        Genres = model.Genres?.Select(x=>x.Name).ToList(),
                        ProductionCountries = model.ProductionCountries?.Select(x=>new CountryDto()
                        {
                            Name = x.Name,
                            IsCode = x.IsoCountryCode
                        }).ToList()
                    }
                };

    public static MoviesSearchRequest ToMoviesByFilterRequest(this MoviesByFilterRequest request)
        => new MoviesSearchRequest()
        {
            ActorPersonName = request.ActorPersonName,
            BudgetMax = request.BudgetMax,
            BudgetMin = request.BudgetMin,
            CrewPersonName = request.CrewPersonName,
            FreeText = request.FreeText,
            GenreName = request.GenreName,
            PageNumber = request.PageNumber,
        };

    public static MovieSearchResponseDto ToResponseDto(this MovieSearchResult result)
        => new MovieSearchResponseDto()
        {
            CurrentPage = result.CurrentPage,
            Movies = result.Movies. Select(x=> new MovieDtoCommon()
            {
                VoteCount = x.VoteCount,
                Budget = x.Budget,
                HomePage = x.HomePage,
                Overview = x.Overview,
                Popularity = x.Popularity,
                ReleaseDate = x.ReleaseDate,
                Revenue = x.Revenue,
                RunTime = x.RunTime,
                Status = x.Status,
                Tagline = x.Tagline,
                Title = x.Name,
                VoteAverage = x.VoteAverage
            }).ToList(),
            TotalPages = (int)Math.Ceiling((decimal)result.TotalResults / (result.PageSize < 1 ? 1 : result.PageSize)),
            TotalResults = result.TotalResults,
        };
}