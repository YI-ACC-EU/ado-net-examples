using Euris.Examples.Common.Models.Dto;
using Euris.Examples.Common.Models.Requests;
using Euris.Examples.Common.Models.Responses;
using Euris.Examples.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace Euris.Examples.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(DefaultMovieResponseDtoCommon), 200)]
    [ProducesResponseType(typeof(string[]), 400)]
    [ProducesResponseType(typeof(string[]), 404)]
    [ProducesResponseType(typeof(string[]), 500)]
    public async Task<IActionResult> Get([FromRoute] MovieByIdRequest request)
        => Map(await _movieService.GetMovieById(request));

    [HttpGet("find")]
    [ProducesResponseType(typeof(MovieSearchResponseDto), 200)]
    [ProducesResponseType(typeof(string[]), 400)]
    [ProducesResponseType(typeof(string[]), 404)]
    [ProducesResponseType(typeof(string[]), 500)]
    public async Task<IActionResult> Filter([FromQuery] MoviesByFilterRequest request)
        => Map( await _movieService.GetMoviesListByFilter(request));
    

    private IActionResult Map<T>(DefaultResponse<T> response)
        => response.StatusCode switch
            {
                200 => Ok(response.Data),
                400 => BadRequest(response.Errors),
                404 => NotFound(response.Errors),
                _ => StatusCode(500, response.Errors)
            };
}