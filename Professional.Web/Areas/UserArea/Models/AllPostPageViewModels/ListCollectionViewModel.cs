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

        public IList<string> FieldsNames { get; set; }

        public string Url { get; set; }

        public string GetBy { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int NextPage
        {
            get
            {
                if (this.CurrentPage >= this.PagesCount)
                {
                    return 1;
                }

                return this.CurrentPage + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (this.CurrentPage <= 1)
                {
                    return this.PagesCount;
                }

                return this.CurrentPage - 1;
            }
        }
    }
}