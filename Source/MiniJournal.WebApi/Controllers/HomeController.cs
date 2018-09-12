using System;
using System.Web.Mvc;

namespace Infotecs.MiniJournal.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            this.ViewBag.Title = "Home Page";

            return this.View();
        }
    }
}
