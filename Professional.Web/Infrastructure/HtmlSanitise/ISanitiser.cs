using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Web.Infrastructure.HtmlSanitise
{
    public interface ISanitiser
    {
        string Sanitize(string html);
        string StripHtml(string source);
    }
}