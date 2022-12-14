using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SIgnalRChatApps.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName
        {
            get;
            set;
        } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        } = string.Empty;
        public bool RememberLogin
        {
            get;
            set;
        }
        public string ReturnUrl
        {
            get;
            set;
        } = string.Empty;
    }
}
