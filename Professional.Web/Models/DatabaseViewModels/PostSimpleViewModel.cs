using AutoMapper;
using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Models
{
    public class PostSimpleViewModel : IMapFrom<Post>
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}