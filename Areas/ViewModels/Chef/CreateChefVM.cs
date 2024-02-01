
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpicyX.Areas.ViewModels.Chef
{
    public class CreateChefVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Ad mutleq daxil edilmelidir")]
        [MaxLength(25, ErrorMessage ="maximum 25 element istifade oluna biler")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Position mutleq daxil edilmelidir")]
        [MaxLength(50, ErrorMessage = "maximum 50 element istifade oluna biler")]
        public string   Position { get; set; }
        public string FbUrl { get; set; }
        public string MailUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string LinkedinUrl { get; set; }
        [NotMapped]
        [Required]
        public IFormFile? Photo { get; set; }


    }
}
