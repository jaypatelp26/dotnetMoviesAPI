using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            this._moviesService = moviesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] int page = 1, [FromQuery] int perPage = 10, [FromQuery] string title = null)
        {
            try
            {
                return Ok(await _moviesService.GetMovies(page, perPage, title));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(string id)
        {
            try
            {
                if (id == null)
                    return BadRequest("id cannot be null");

                if (await _moviesService.GetMovieById(id) == null)
                    return BadRequest($"Movie with Id = {id} not found");

                return Ok(await _moviesService.GetMovieById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] Movie movie)
        {
            try
            {
                await _moviesService.AddMovie(movie);
                return Ok($"Movie with Id = {movie.Id} added.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieById([FromBody] Movie movie, string id)
        {
            try
            {
                if (await _moviesService.GetMovieById(id) == null)
                    return BadRequest($"Movie with Id = {id} not found");

                movie.Id = id;
                await _moviesService.UpdateMovieById(movie, id);
                return Ok($"Movie with Id = {id} updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieById(string id)
        {
            try
            {
                if (await _moviesService.GetMovieById(id) == null)
                    return BadRequest($"Movie with Id = {id} not found");

                await _moviesService.DeleteMovieById(id);
                return Ok($"Movie with Id = {id} deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
