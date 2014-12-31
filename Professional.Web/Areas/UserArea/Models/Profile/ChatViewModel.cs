using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models
{
    public class ChatViewModel
    {
        public string ToUserId { get; set; }
        public IEnumerable<string> Messages { get; set; }
    }
}