using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_63131631.Controllers
{
    public class Errors_63131631Controller : Controller
    {
        //
        // GET: /Errors/
        [HandleError]
        public ActionResult Index()
        {
            return View();
        }
        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
	}
}