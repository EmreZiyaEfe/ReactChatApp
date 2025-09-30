using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class User : EntityBase
    {
        [Required]
        [MaxLength(15)]
        public string NickName { get; set; }

        //Nav Property
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
