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
        public int CleranceLevelId { get; set; }

        public virtual ControlLevel CleranceLevel { get; set; }
    }
}