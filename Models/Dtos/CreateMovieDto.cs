using System.ComponentModel.DataAnnotations;

namespace API_Movies.Models.Dtos
{
    public class CreateMovieDto
    {
        public string MovieName { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string ImgFilePath { get; set; }
        public enum CreateClasificationType { Seven, Thirteen, Sixteen, Eighteen }
        public CreateClasificationType Clasification { get; set; }

        public int CategoryId { get; set; }
    }
}
