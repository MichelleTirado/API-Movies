using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Movies.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string ImgFilePath { get; set; }
        public enum ClasificationType { Seven, Thirteen, Sixteen, Eighteen }
        public ClasificationType Clasification { get; set; }
        public DateTime CreationDate { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
