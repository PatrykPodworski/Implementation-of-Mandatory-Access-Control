using System.ComponentModel.DataAnnotations;

namespace bsk2v2.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public int ClassificationLevelId { get; set; }

        public ControlLevel ClassificationLevel { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public User Author { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }
    }
}