using API_Movies.Models;

namespace API_Movies.Repositories.IRepositories
{
    public interface IMovie
    {
        ICollection<Movie> GetMovies();
        ICollection<Movie> GetMoviesByCategory(int categoryId);
        IEnumerable<Movie> GetMoviesByName(string movieName);
        Movie GetMovieById(int movieId);
        bool ExistsMovieById(int movieId);
        bool ExistsMovieByName(string movieName);
        bool CreateMovie(Movie movie);
        bool UpdateMovie(Movie movie);
        bool DeleteMovie(Movie movie);
        bool SaveMovie();

    }
}
