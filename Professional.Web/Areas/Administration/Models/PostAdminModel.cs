using AutoMapper;
using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.Administration.Models
{
    public class PostAdminModel : AdministrationViewModel, IMapFrom<Post>
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [AllowHtml]
        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }
    }
}