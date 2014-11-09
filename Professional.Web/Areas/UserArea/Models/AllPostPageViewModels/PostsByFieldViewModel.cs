using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Professional.Models;

namespace Professional.Web.Areas.UserArea.Models
{
    public class PostsByFieldViewModel
    {
        public string Name { get; set; }
        public IList<Post> Posts { get; set; }
    }
}