namespace Professional.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    using Professional.Data;

    public class ImageController : BaseController
    {
        public ImageController(IApplicationData data)
            : base(data)
        {
        }

        // GET: Image
        public ActionResult ImageById(int id)
        {
            var image = this.data.Images.GetById(id);
            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return this.File(image.Content, "image/" + image.FileExtension);
        }
    }
}