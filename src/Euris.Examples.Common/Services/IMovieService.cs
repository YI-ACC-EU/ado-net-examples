using Euris.Examples.Common.Models.Dto;
using Euris.Examples.Common.Models.Requests;
using Euris.Examples.Common.Models.Responses;

namespace Euris.Examples.Common.Services;

public interface IMovieService
{
    public Task<DefaultResponse<MovieResponseDto>> GetMovieById(MovieByIdRequest request);
}