using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WWD.Models;
namespace WWD.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        DonationDB ctx = new DonationDB();
        //bitmedi :(
        public ActionResult Index(Guid id)
        {
            var data = ctx.Kullanicis.FirstOrDefault(x => x.KullaniciId == id);
            ViewBag.bekleyen = ctx.Kampanyas.Where(x => x.Baslatan == id).Where(y => y.OnaylandiMi == false && y.BasariliMi==false).ToList();
            ViewBag.onaylanmis = ctx.Kampanyas.Where(x => x.Baslatan == id).Where(y => y.OnaylandiMi == true &&y.BasariliMi==false).ToList();
            ViewBag.bitmis = ctx.Kampanyas.Where(x => x.Baslatan == id).Where(z => z.BasariliMi == true).ToList();
            ViewBag.destekleri = ctx.Donates.Where(x => x.GonderenID == id).ToList();
           // var sd = ctx.Donates.Where(a => a.GonderenID == id);
            ViewBag.gelendestek = ctx.Donates.Where(d =>d.Kampanya.Baslatan == id&&d.GonderdiMi==false).Except(ctx.Donates.Where(a => a.GonderenID == id && a.GonderdiMi==true)).ToList();
            ViewBag.temp = TempData["kargo"];
            ViewBag.hata = TempData["lan"];
            return View(data);
        }
        //Silme Action u yazılmadı&bu action kampanya controllerında , ajax ile gidiyor
        // public ActionResult KampanyaSil(int id)
        //{
        //  return View();
        //}
        [HttpPost]
        public ActionResult Editme(Guid id,string eposta, HttpPostedFileBase foto)
        {
            Guid ad = id;
            if (eposta==""&&foto==null)
            {
                TempData["lan"] = "Fill the Gaps";
                return RedirectToAction("Index/" + ad);
            }
            if (eposta!="")
            {
                MembershipUser ads = Membership.GetUser(id);
                Kullanici kal = ctx.Kullanicis.FirstOrDefault(x => x.KullaniciId == id);
                
                ads.Email = eposta;
                kal.Email = ads.Email;
            }
            if (foto!=null)
            {
                Kullanici kal = ctx.Kullanicis.FirstOrDefault(x => x.KullaniciId == id);

                Image profoto = Image.FromStream(foto.InputStream);
                int width = Convert.ToInt32(ConfigurationManager.AppSettings["profilW"]);
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["profilH"]);
                string name = "/Content/Uploads/ProfilFoto/" + Guid.NewGuid() + Path.GetExtension(foto.FileName);
                Bitmap bm = new Bitmap(profoto, width, height);
                bm.Save(Server.MapPath(name));
                kal.ProfilFoto = name;
            }
            
            ctx.SaveChanges();
            
          
            return RedirectToAction("Index/" + ad);
        }
    }
}