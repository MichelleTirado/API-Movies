using System.ComponentModel.DataAnnotations;

namespace API_Movies.Models.Dtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage="Name is required")]
        [MaxLength(100, ErrorMessage = "Max length is 100 characters")]
        public string CategoryName { get; set; }
    }
}
