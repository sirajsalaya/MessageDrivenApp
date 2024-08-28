namespace MessageDrivenApp.Models
{
    public class Message(string content)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; } = content;
        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
    }
}