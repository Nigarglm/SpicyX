using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpicyX.Models
{
    public class Chef
    {
        public int Id { get; set; }
        //[Required]
        [MaxLength(25)]
        public string Name { get; set; }
        //[Required]
        [MaxLength(50)]
        public string Position { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        public string FbUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string MailUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public int PositionId { get; set; }
    }
}
