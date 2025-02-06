using API_Movies.Models.Dtos;
using API_Movies.Models;
using API_Movies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Movies.Repositories;

namespace API_Movies.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovie _movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovie movieRepo, IMapper mapper)
        {
            _movieRepository = movieRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMovies()
        {
            var movies = _movieRepository.GetMovies();

            var moviesDto = new List<MovieDto>();

            foreach (var movie in movies)
            {
                moviesDto.Add(_mapper.Map<MovieDto>(movie));
            }

            return Ok(moviesDto);
        }

        [HttpGet("{movieId:int}", Name = "GetMovieById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMovieById(int movieId)
        {
            var itemMovie = _movieRepository.GetMovieById(movieId);

            if (itemMovie == null)
            {
                return NotFound();
            }

            var itemMovieDto = _mapper.Map<MovieDto>(itemMovie);

            return Ok(itemMovieDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateMovie([FromBody] CreateMovieDto movie)
        {
            if (!ModelState.IsValid || movie == null)
            {
                return BadRequest(ModelState);
            }

            if (_movieRepository.ExistsMovieByName(movie.MovieName))
            {
                ModelState.AddModelError("", $"Movie already exists");
                return StatusCode(404, ModelState);
            }

            var movieFromBody = _mapper.Map<Movie>(movie);

            if (!_movieRepository.CreateMovie(movieFromBody))
            {
                ModelState.AddModelError("", $"Creation failed: {movie.MovieName}");
                return StatusCode(404, ModelState);
            }

            return CreatedAtRoute("GetMovieById", new { movieId = movieFromBody.Id }, movieFromBody);
        }

        [HttpPatch("{movieId:int}", Name = "UpdateMovieById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateMovieById(int movieId, [FromBody] MovieDto movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (movie == null || movieId != movie.Id)
            {
                return BadRequest(ModelState);
            }

            var movieExists = _movieRepository.GetMovieById(movieId);

            if(movieExists == null)
            {
                return NotFound($"Movie does not exists");
            }

            var movieFromBody = _mapper.Map<Movie>(movie);

            if (!_movieRepository.UpdateMovie(movieFromBody))
            {
                ModelState.AddModelError("", $"Update failed: {movie.MovieName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{movieId:int}", Name = "DeleteMovie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteMovie(int movieId)
        {

            if (!_movieRepository.ExistsMovieById(movieId))
            {
                return NotFound();
            }

            var movie = _movieRepository.GetMovieById(movieId);

            if (!_movieRepository.DeleteMovie(movie))
            {
                ModelState.AddModelError("", $"Delete failed: {movie.MovieName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet("GetMoviesByCategory/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMoviesByCategory(int categoryId)
        {
            var movieList = _movieRepository.GetMoviesByCategory(categoryId);

            if(movieList == null)
            {
                return NotFound();
            }

            var itemMovie = new List<MovieDto>();

            foreach(var movie in movieList)
            {
                itemMovie.Add(_mapper.Map<MovieDto>(movie));
            }

            return Ok(itemMovie);
        }

        [HttpGet("GetMoviesByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetMoviesByName(string movieName)
        {
            try
            {
                var response = _movieRepository.GetMoviesByName(movieName);

                if(response.Any())
                {
                    return Ok(response);
                }

                return NotFound();

            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get results");
            }
           
        }
    }


}
