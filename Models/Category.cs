﻿using System.ComponentModel.DataAnnotations;

namespace API_Movies.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
    }
}
