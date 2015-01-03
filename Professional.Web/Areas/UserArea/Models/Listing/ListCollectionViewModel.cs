namespace Professional.Web.Areas.UserArea.Models.Listing
{
    using System.Collections.Generic;

    public class ListCollectionViewModel
    {
        public string Title { get; set; }

        public IList<ItemsByFieldViewModel> Fields { get; set; }

        public IList<string> FieldsNames { get; set; }

        public string GetBy { get; set; }

        public bool WithImage { get; set; }
    }
}