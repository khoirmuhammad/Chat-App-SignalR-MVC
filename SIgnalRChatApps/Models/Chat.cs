namespace SIgnalRChatApps.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsMessageSender { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
