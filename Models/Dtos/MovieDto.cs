using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Movies.Models.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string ImgFilePath { get; set; }
        public enum ClasificationType { Seven, Thirteen, Sixteen, Eighteen }
        public ClasificationType Clasification { get; set; }
        public DateTime CreationDate { get; set; }

        public int CategoryId { get; set; }
    }
}
