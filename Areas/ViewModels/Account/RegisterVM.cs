
using System.ComponentModel.DataAnnotations;

namespace SpicyX.Areas.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [MaxLength(20, ErrorMessage = "En cox 20 element istifade oluna biler")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [MaxLength(20, ErrorMessage = "En cox 20 element istifade oluna biler")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [MaxLength(20, ErrorMessage = "En cox 20 element istifade oluna biler")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
