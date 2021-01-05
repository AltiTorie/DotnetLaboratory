using System;
using System.ComponentModel.DataAnnotations;

namespace Lab11.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel() {}

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
        [Required(ErrorMessage = "Username is Required")]
        public String Name { get; set; }

        [MaxLength(80, ErrorMessage = "Email must not exceed 80 characters")]
        [EmailAddress]
        public String Email { get; set; }

        [Required(ErrorMessage = "Post code is Required")]
        [RegularExpression(@"^\d{2}(-\d{3})$", ErrorMessage = "Invalid Post code")]
        public String PostCode { get; set; }

        [Required(ErrorMessage = "User category is required")]
        public Category Category { get; set; }


        // 1. Stworzyć dowolną klasę modelu widoków zawierającą wśród właściwości adres
        // email, nazwę(maksymalnie 40 znaków), kod pocztowy, kategorię (jako typ
        // wyliczeniowy enum). Użyć adnotacji do sprawdzania poprawności danych w
        // modelu.Przygotować zrozumiałe dla użytkownikach informacje o wpisaniu
        // niepoprawnych danych w formularzach.


    }
    public enum Category
    {
        Seller,
        Buyer,
        Admin,
        Moderator
    }
}
