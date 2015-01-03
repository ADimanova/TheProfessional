namespace Professional.Web.Areas.UserArea.Models
{
    using System.Collections.Generic;

    public class ChatViewModel
    {
        public string ToUserId { get; set; }

        public IEnumerable<string> Messages { get; set; }
    }
}