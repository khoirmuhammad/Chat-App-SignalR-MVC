namespace SIgnalRChatApps.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
    }
}
