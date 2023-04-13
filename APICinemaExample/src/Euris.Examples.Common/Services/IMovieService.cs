using Euris.Examples.Common.Models.Dto;
using Euris.Examples.Common.Models.Requests;
using Euris.Examples.Common.Models.Responses;

namespace Euris.Examples.Common.Services;

public interface IMovieService
{
    public Task<DefaultResponse<DefaultMovieResponseDtoCommon>> GetMovieById(MovieByIdRequest request);
    public Task<DefaultResponse<MovieSearchResponseDto>> GetMoviesListByFilter(MoviesByFilterRequest request);
}