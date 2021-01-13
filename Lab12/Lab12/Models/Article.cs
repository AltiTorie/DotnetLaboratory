using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab12.Models
{
    public class Article
    {

        public int Id { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Article name must exceed 4 characters")]
        [MaxLength(255, ErrorMessage = "Article name cannot exceed 255 characters")]
        public string Name { get; set; }
        [Required]
        [Range(0, double.PositiveInfinity)]
        public double Price { get; set; }
        [Display(Name = "Image")]

        public string PathToImage { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
    }
}
