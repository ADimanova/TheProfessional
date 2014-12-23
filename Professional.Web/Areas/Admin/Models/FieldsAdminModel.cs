using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.Admin.Models
{
    public class FieldsAdminModel
    {
        public IEnumerable<FieldAdminModel> Fields;
        public FieldAdminModel SelectedField;
    }
}