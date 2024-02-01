using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpicyX.Models
{
    public class Chef
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ad mutleq daxil edilmelidir")]
        [MaxLength(25,ErrorMessage ="Ad ucun maksimum uzunluq 25-dir")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Position mutleq daxil edilmelidir")]
        [MaxLength(50,ErrorMessage ="Position ucun maximum uzunluq 50-dir")]
        public string Position { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [Required(ErrorMessage ="Shekil mutleq elave edilmelidir")]
        public IFormFile? Photo { get; set; }
        public string FbUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string MailUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public int PositionId { get; set; }
    }
}
