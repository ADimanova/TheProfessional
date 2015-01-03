namespace Professional.Web.Areas.UserArea.Models
{
    public class MessageViewModel
    {
        public string FromUserId { get; set; }

        public string FromUserName { get; set; }

        public string Preview { get; set; }

        public bool IsRead { get; set; }
    }
}