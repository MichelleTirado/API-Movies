using API_Movies.Models;
using API_Movies.Models.Dtos;
using AutoMapper;

namespace API_Movies.MapperMovies
{
    public class MapperMovies : Profile
    {
        public MapperMovies() {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();

            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Movie, CreateMovieDto>().ReverseMap();
        }

    }
}
