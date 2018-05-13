using System.ComponentModel.DataAnnotations;

namespace bsk2v2.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int ControlLevelId { get; set; }

        public ControlLevel ControlLevel { get; set; }
    }
}