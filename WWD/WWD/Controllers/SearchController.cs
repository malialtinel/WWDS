using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WWD.Models;

namespace WWD.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        DonationDB ctx = new DonationDB();
        public ActionResult Index()
        {
         
           ViewBag.HAYDA = TempData["haydarhaydar"];
            return View();
        }
        public ActionResult Arama(string haydar)
        {
           List<Kampanya> haydari = ctx.Kampanyas.Where(x => x.Adi.Contains(haydar)||x.Kategori.KategoriAdi.Contains(haydar)).ToList();
            TempData["haydarhaydar"] = haydari;
            return RedirectToAction("Index");
        }

    }
}