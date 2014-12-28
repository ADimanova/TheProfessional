using Professional.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Controllers
{
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

            return File(image.Content, "image/" + image.FileExtension);
        }
    }
}