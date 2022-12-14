namespace SIgnalRChatApps.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public List<UserModel> GetUser()
        {
            List<UserModel> users = new List<UserModel>();

            users.Add(new UserModel()
            {
                UserId = Guid.NewGuid(),
                Username = "muhammadkhoirudin",
                Fullname = "Muhammad Khoirudin",
                Password = "password",
            });
            users.Add(new UserModel()
            {
                UserId = Guid.NewGuid(),
                Username = "naskalaberyl",
                Fullname = "Naskala Beryl Alfathlan",
                Password = "password",
            });
            users.Add(new UserModel()
            {
                UserId = Guid.NewGuid(),
                Username = "deaalvine",
                Fullname = "Dea Alvine Lutfiani",
                Password = "password",
            });

            return users;
        }
    }
}
