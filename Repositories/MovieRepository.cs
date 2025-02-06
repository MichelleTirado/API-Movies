using API_Movies.Data;
using API_Movies.Models;
using API_Movies.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API_Movies.Repositories
{
    public class MovieRepository : IMovie
    {
        private readonly AppDbContext _db;

        public MovieRepository(AppDbContext db)
        {
            _db = db;
        }

        public bool CreateMovie(Movie movie)
        {
            movie.CreationDate = DateTime.Now;
            _db.Movie.Add(movie);
            return SaveMovie();
        }

        public bool UpdateMovie(Movie movie)
        {
            movie.CreationDate = DateTime.Now;

            var movieExists = _db.Movie.Find(movie.Id);

            if (movieExists != null)
            {
                _db.Entry(movieExists).CurrentValues.SetValues(movie);
            }
            else
            {
                _db.Movie.Update(movie);
            }

            return SaveMovie();
        }

        public bool DeleteMovie(Movie movie)
        {
            _db.Movie.Remove(movie);
            return SaveMovie();
        }

        public bool ExistsMovieById(int movieId)
        {
            return _db.Movie.Any(c => c.Id == movieId);
        }

        public bool ExistsMovieByName(string movieName)
        {
            bool value = _db.Movie.Any(c => c.MovieName.ToLower().Trim() == movieName.ToLower().Trim());
            return value;
        }

        public ICollection<Movie> GetMovies()
        {
           return _db.Movie.OrderBy(c => c.MovieName).ToList();
        }

        public Movie GetMovieById(int movieId)
        {
            return _db.Movie.FirstOrDefault(c => c.Id == movieId);
        }

        public bool SaveMovie()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public ICollection<Movie> GetMoviesByCategory(int categoryId)
        {
            return _db.Movie.Where(cat => cat.CategoryId == categoryId).ToList();
        }

        public IEnumerable<Movie> GetMoviesByName(string movieName)
        {
            IQueryable<Movie> query = _db.Movie;

            if (!string.IsNullOrEmpty(movieName))
            {
                query = query.Where(m => m.MovieName.Contains(movieName));
            }
            return query.ToList();
        }
    }
}
