namespace Backend.Models.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string SentimentLabel { get; set; }
    }
}
