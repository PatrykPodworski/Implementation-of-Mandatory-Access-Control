using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bsk2v2.Models
{
    public class Report
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Desription { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public User Author { get; set; }

        [Required]
        public int ControlLevelId { get; set; }

        public ControlLevel ControlLevel { get; set; }

        public List<Document> Documents { get; set; }
    }
}