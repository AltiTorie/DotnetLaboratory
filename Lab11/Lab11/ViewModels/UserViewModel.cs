using System;
using System.ComponentModel.DataAnnotations;

namespace Lab11.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel() { }

        public UserViewModel(int id, string name, string email, string postCode, Category category)
        {
            Id = id;
            Name = name;
            Email = email;
            PostCode = postCode;
            Category = category;
        }

        public int Id { get; set; }

        [MinLength(5, ErrorMessage = "Username too short")]
        [Display(Name = "Username")]
        [MaxLength(40, ErrorMessage = "Username must not exceed 40 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is Required")]
        public String Name { get; set; }

        [MaxLength(80, ErrorMessage = "Email must not exceed 80 characters")]
        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [RegularExpression(@"^\d{2}(-\d{3})$", ErrorMessage = "Invalid Post code")]
        [Display(Name = "Post code")]
        public String PostCode { get; set; }

        [Required(ErrorMessage = "User category is required")]
        public Category Category { get; set; }

    }
    public enum Category
    {
        Seller,
        Buyer,
        Admin,
        Moderator
    }
}
