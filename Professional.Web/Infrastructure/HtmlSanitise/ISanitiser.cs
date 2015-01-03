namespace Professional.Web.Infrastructure.HtmlSanitise
{
    public interface ISanitiser
    {
        string Sanitize(string html);

        string StripHtml(string source);
    }
}