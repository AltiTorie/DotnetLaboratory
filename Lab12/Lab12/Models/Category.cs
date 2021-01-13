using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Category name must exceed than 2 characters")]
        [MaxLength(20, ErrorMessage ="Category name cannot exceed 20 characters")]
        public String Name { set; get; }
    }
}
