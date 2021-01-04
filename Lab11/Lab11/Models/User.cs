using System;
using System.ComponentModel.DataAnnotations;

namespace Lab11.Models
{
    public class User
    {
        [Key]
        [MaxLength(40, ErrorMessage = "Name must not exceed 40 characters")]
        [Required(ErrorMessage = "Name is Required")]
        private String Name { get; set; }

        [MaxLength(80, ErrorMessage = "Email must not exceed 80 characters")]
        [EmailAddress]
        private String Email { get; set; }
                
        [Required(ErrorMessage = "Post code is Required")]
        [RegularExpression(@"^\d{2}(-\d{3})$", ErrorMessage = "Invalid Post code")]
        private String PostCode { get; set;}

        [Required(ErrorMessage = "Choose user category")]
        private Category Category { get; set; }



//        1. Stworzyć dowolną klasę modelu widoków zawierającą wśród właściwości adres
//email, nazwę(maksymalnie 40 znaków), kod pocztowy, kategorię (jako typ
//wyliczeniowy enum). Użyć adnotacji do sprawdzania poprawności danych w
//modelu.Przygotować zrozumiałe dla użytkownikach informacje o wpisaniu
//niepoprawnych danych w formularzach.


    }
    public enum Category
    {
        Seller,
        Buyer,
        Admin,
        Moderator
    }
}
