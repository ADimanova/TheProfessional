using System;
using System.Linq;
using System.Web.Mvc;
using Professional.Data;

namespace Professional.Web.Controllers
{
    public class BaseController : Controller
    {
        IApplicationData data;

        public BaseController(IApplicationData data)
        {
            this.data = data;
        }

        public BaseController()
            : this(new ApplicationData())
        {
        }
    }
}