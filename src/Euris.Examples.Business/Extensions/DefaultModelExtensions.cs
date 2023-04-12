using Euris.Examples.Common.Models.Dto;
using Euris.Examples.Common.Models.Entities;
using Euris.Examples.Common.Models.Responses;

namespace Euris.Examples.Business.Extensions;

public static class DefaultModelExtensions
{
    public static DefaultResponse<MovieResponseDto> ToResponse(this Movie? model)
        => model is null
            ? new DefaultResponse<MovieResponseDto>()
                {
                    StatusCode = 404,
                    Errors = new[] {"Not found"}
                }
            : new DefaultResponse<MovieResponseDto>()
                {
                    StatusCode = 200,
                    Data =  new MovieResponseDto()
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
}