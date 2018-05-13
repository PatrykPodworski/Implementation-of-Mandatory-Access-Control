using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bsk2v2.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        public int ControlLevelId { get; set; }

        public ControlLevel ControlLevel { get; set; }

        [Required]
        public int UserId { get; set; }

        public User Author { get; set; }

        [Required]
        public string Text { get; set; }

        public List<Report> Reports { get; set; }
    }
}