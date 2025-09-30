using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Message : EntityBase
    {
        [Required]
        [MaxLength(500)]
        public string Text { get; set; }
        [Required]
        public int UserId { get; set; }
        [MaxLength(20)]
        public string? SentimentLabel { get; set; }
        public double? SentimentScore { get; set; }

        //Nav property
        public User? User { get; set; }
    }
}
