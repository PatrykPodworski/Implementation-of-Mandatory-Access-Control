using System.ComponentModel.DataAnnotations;

namespace bsk2v2.Models
{
    public class ControlLevel
    {
        public int Id { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public string Name { get; set; }
    }
}