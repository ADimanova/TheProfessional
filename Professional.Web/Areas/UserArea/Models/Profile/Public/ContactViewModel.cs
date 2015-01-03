namespace Professional.Web.Areas.UserArea.Models.Profile.Public
{
    public class ContactViewModel
    {
        public string FromUserId { get; set; }

        public string FromUserName { get; set; }

        public bool IsConnected { get; set; }

        public bool IsAccepted { get; set; }
    }
}