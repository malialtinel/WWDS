using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WWD.Models;
namespace WWD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DonationDB ctx = new DonationDB();
        public ActionResult Index()

        {
            ViewBag.gulu = ctx.Kullanicis.ToList().Count();
            ViewBag.lmet = ctx.Donates.ToList().Count();
            ViewBag.templ = ctx.Kampanyas.Where(x => x.OnaylandiMi == true).ToList().Count();
            var ca = ctx.Kampanyas.Where(x => x.OnaylandiMi == true && x.BasariliMi == false).ToList();
            return View(ca);
        }
    }
}