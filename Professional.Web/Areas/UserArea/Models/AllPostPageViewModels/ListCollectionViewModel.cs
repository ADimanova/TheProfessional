using AutoMapper;
using Professional.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models
{
    public class ListCollectionViewModel
    {
        public string Title { get; set; }
        public IList<ItemsByFieldViewModel> Fields { get; set; }
    }
}