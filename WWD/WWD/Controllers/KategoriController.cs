using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WWD.Models;

namespace WWD.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DonationDB ctx = new DonationDB();
        public ActionResult Index()
        {
            ViewBag.kat1 = ctx.Kategoris.Take(12).ToList();
           
            return View();
        }
        public ActionResult KategoriDetay(int id)
        {
            var data = ctx.Kampanyas.Where(x=>x.KategoriID==id&&x.OnaylandiMi==true).ToList();
            return View(data);
        }

        public ActionResult Yukle()
        {
            ViewBag.kat = ctx.Kategoris.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Yukle(int KategoriId, HttpPostedFileBase img)
        {
            Kategori ktg = ctx.Kategoris.FirstOrDefault(x=>x.KategoriId==KategoriId);
            
            string name = "/Content/Uploads/Olaylar/" + Guid.NewGuid() + Path.GetExtension(img.FileName);

            ktg.ResimYolu = name;
            img.SaveAs(Server.MapPath(name));
            ctx.SaveChanges();
            return View();
        }
    }
}