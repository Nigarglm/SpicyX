using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpicyX.Areas.ViewModels.Chef
{
    public class CreateChefVM
    {
        public int Id { get; set; }
        //[Required(ErrorMessage="Ad mutleq daxil edilmelidir")]
        [MaxLength(25, ErrorMessage ="maximum 25 element istifade oluna biler")]
        public string Name { get; set; }
        public string   Position { get; set; }
        [NotMapped]
        //[Required]
        public IFormFile? Photo { get; set; }

    }
}
