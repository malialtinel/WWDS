using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WWD.Models;
namespace WWD.Controllers
{
    public class KampanyaController : Controller
    {
        // GET: Kampanya
        DonationDB ctx = new DonationDB();
        [Authorize(Roles = "UYE")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated != true)
            {
                TempData["adad"] = "You must Log in";
            }
            ViewBag.kategoriler = ctx.Kategoris.ToList();

            return View();
        }


        public ActionResult KampanyaDetay(int id)
        {
            ViewBag.katwid = ctx.Kategoris.Take(12).OrderByDescending(x => x.KategoriId);
            var data = ctx.Kampanyas.FirstOrDefault(x => x.KampanyaId == id);
            ViewBag.addd = ctx.Stuffs.Where(x => x.KampanyaID == id);
            ViewBag.fu = ctx.Resims.Where(x => x.KampanyaID == id).Take(4).OrderByDescending(y => y.ResimId).ToList();
            return View(data);
        }
        //Çoklu resimi dene
        [Authorize(Roles = "UYE")]

        public ActionResult KampanyaBaslat(Kampanya ka, HttpPostedFileBase kampresim, string[] StuffName, int[] Miktari, string[] Birim, HttpPostedFileBase[] ResimID)
        {

            if (kampresim != null)
            {
                Image img = Image.FromStream(kampresim.InputStream);
                int width = Convert.ToInt32(ConfigurationManager.AppSettings["kampKucukW"]);
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["kampKucukh"]);
                string name = "/Content/Uploads/KucukFoto" + Guid.NewGuid() + Path.GetExtension(kampresim.FileName);
                Bitmap bm = new Bitmap(img, width, height);
                bm.Save(Server.MapPath(name));
                ka.KampanyaFoto = name;
                int wd = Convert.ToInt32(ConfigurationManager.AppSettings["kampBuyukW"]);
                int hd = Convert.ToInt32(ConfigurationManager.AppSettings["kampBuyukH"]);
                string name2 = "/Content/Uploads/BuyukFoto" + Guid.NewGuid() + Path.GetExtension(kampresim.FileName);
                Bitmap bm2 = new Bitmap(img, wd, hd);
                bm.Save(Server.MapPath(name2));
                ka.KampanyaFotoB = name2;
            }
            //eşya nesnesini yukarıda oluşturmamım bir sebebi var ama ne mk
            Stuff esya = new Stuff();
            Guid baslatanId = new Guid();
            baslatanId = ctx.Kullanicis.FirstOrDefault(x => x.UserAdi == User.Identity.Name).KullaniciId;
            ka.Baslatan = baslatanId;
            ka.BasariliMi = false;
            ka.OnaylandiMi = false;
            ka.BaslamaTarihi = DateTime.Now;

            // ka.StuffID = 1;
            //ka.DonateID = 0;

            ctx.Kampanyas.Add(ka);
            ctx.SaveChanges();
            int d = ka.KampanyaId;
            Resim rsm = new Resim();
            if (ResimID[0] != null)
            {
                for (int i = 0; i < ResimID.Count(); i++)
                {
                    string name = "/Content/Uploads/Olaylar/" + Guid.NewGuid() + Path.GetExtension(ResimID[i].FileName);

                    // Image img = Image.FromStream(ResimID[i].InputStream);
                    //  Bitmap bm = new Bitmap(img);   
                    ResimID[i].SaveAs(Server.MapPath(name));
                    rsm.ResimBuyukYol = name;
                    rsm.ResimKucukYol = name;
                    rsm.KampanyaID = d;
                    ctx.Resims.Add(rsm);
                    ctx.SaveChanges();
                }
            }

            if (rsm.ResimId != 0)
            {
                ka.ResimID = rsm.ResimId;

            }

            for (int i = 0; i < StuffName.Count() && i < Birim.Count() && i < Miktari.Count(); i++)
            {
                esya.Birim = Birim[i];
                esya.Miktari = Miktari[i];
                esya.StuffName = StuffName[i];

                esya.KampanyaID = 0;
                esya.KalanMiktar = 0;
                if (esya.KampanyaID != -1)
                {
                    esya.KampanyaID = d;
                }
                ctx.Stuffs.Add(esya);
                try
                {
                    ctx.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
            }
            // ctx.Kampanyas.Add(ka);
            //AHANDA BURADA HATA VERİYOR
            // ka.StuffID = esya.StuffId;
            // ctx.Kampanyas.Add(ka);
            ctx.SaveChanges();
            return RedirectToAction("KampanyaDetay/" + ka.KampanyaId);
        }
        //Kargo Takip No su Denenmedi
        [Authorize(Roles = "UYE")]
        [HttpPost]
        public ActionResult BagisYap(int[] GonderiMiktari, int[] StuffID, int[] KampanyaID)
        {
            Donate gelen = new Donate();
            if (User.Identity.IsAuthenticated)
            {
                for (int i = 0; i < GonderiMiktari.Count() && i < StuffID.Count() && i < KampanyaID.Count(); i++)
                {
                    gelen.GonderiMiktari = GonderiMiktari[i];
                    gelen.KampanyaID = KampanyaID[i];
                    gelen.StuffID = StuffID[i];
                    Guid bagisci = new Guid();
                    bagisci = ctx.Kullanicis.FirstOrDefault(x => x.UserAdi == User.Identity.Name).KullaniciId;
                    gelen.GonderdiMi = false;
                    gelen.VardiMi = false;
                    gelen.GonderenID = bagisci;
                    gelen.BasladiMi = true;
                    if (gelen.GonderiMiktari == 0)
                    {
                        gelen.GonderiMiktari = 0;
                        gelen.StuffID = 41;
                        gelen.KampanyaID = 65;
                        bagisci = ctx.Kullanicis.FirstOrDefault(x => x.UserAdi == "sell").KullaniciId;
                        gelen.GonderenID = bagisci;
                        gelen.GonderdiMi = false;
                        gelen.VardiMi = false;
                        gelen.BasladiMi = true;
                    }
                    ctx.Donates.Add(gelen);
                    ctx.SaveChanges();
                }

                //kargo takip nosunu null yap,
                // gelen.KargoTakipNo = a + "WWD" + b;
                //   ViewBag.kargotakip = gelen.KargoTakipNo;
                // ctx.Donates.Add(gelen);
                //   ctx.SaveChanges();
                //   ViewBag.kargotakip
            }
            int a = gelen.DonateId;
            int b = gelen.KampanyaID;
            TempData["kargo"] = a + "WWD" + b;
            string c = HttpContext.User.Identity.Name;
            Guid id = (Guid)Membership.GetUser(c).ProviderUserKey;
            return RedirectToAction("Index", "Kullanici", new { @id = id });
        }
        //db yi değiştirdim donate ve stuff arasına relation ve stuff a coloumn ekledim
        [Authorize(Roles = "UYE")]
        [HttpPost]
        public void Onayla(int id, int tr)
        {
            Stuff fd = ctx.Stuffs.FirstOrDefault(x => x.StuffId == tr);
            Donate dt = ctx.Donates.FirstOrDefault(x => x.DonateId == id);
            // Stuff sta = ctx.Stuffs.FirstOrDefault(y=>y.KampanyaID==dt.KampanyaID);
            Kampanya kak = ctx.Kampanyas.FirstOrDefault(x => x.KampanyaId == dt.KampanyaID);
            List<Stuff> daat = ctx.Stuffs.Where(x => x.KampanyaID == dt.KampanyaID).ToList();
            if (dt.Stuff.StuffId == tr)
            {
                int d = fd.Miktari;
                int a = dt.GonderiMiktari;
                int b = fd.KalanMiktar;//Giden miktar
                int c = d - a;

                // dt.Stuff.KalanMiktar = b + a;
                if (c >= 0)
                {
                    fd.KalanMiktar = b + a;
                    if (b != d)
                    {
                        int jaasdd = 0;
                        for (int i = 0; i < daat.Count(); i++)
                        {
                            // int[] ad;
                            // List<Stuff> adsad = ctx.Stuffs.Where(x => x.KampanyaID == sta.KampanyaID).ToList();
                            int f = daat[i].KalanMiktar;
                            int g = daat[i].Miktari;

                            int aff = g - f;
                           // aff -= 1;
                           
                            if (aff == 0)
                            {
                                jaasdd = jaasdd + 1;
                            }
                            if (jaasdd == daat.Count())
                            {
                                kak.BasariliMi = true;
                                ctx.SaveChanges();
                            }

                        }
                        dt.GonderdiMi = true;


                        ctx.SaveChanges();
                    }
                }
            }
        }
        //ajax kullandım
        [Authorize(Roles = "UYE")]
        [HttpPost]
        public void Red(int id)
        {
            Donate dt = ctx.Donates.FirstOrDefault(x => x.DonateId == id);
            ctx.Donates.Remove(dt);
            ctx.SaveChanges();

        }
        [Authorize(Roles = "UYE")]
        [HttpPost]
        public ActionResult GlnBagsOnay(int id, string KargoTakipNo)
        {
            Donate dd = ctx.Donates.FirstOrDefault(x => x.DonateId == id);

            dd.KargoTakipNo = KargoTakipNo;
            dd.VardiMi = true;
            ctx.SaveChanges();

            string a = HttpContext.User.Identity.Name;
            Guid add = (Guid)Membership.GetUser(a).ProviderUserKey;

            return RedirectToAction("Index", "Kullanici", new { @id = add });
        }
        [Authorize(Roles = "UYE")]
        [HttpPost]
        public void KampanyaSil(int id)
        {
            Kampanya kk = ctx.Kampanyas.FirstOrDefault(x => x.KampanyaId == id);
            ctx.Kampanyas.Remove(kk);
            ctx.SaveChanges();

        }
        [Authorize(Roles = "UYE")]
        public ActionResult Yenile()
        {

            return View();
        }
        [Authorize(Roles = "UYE")]
        [HttpPost]
        public ActionResult Yenile(int id, string[] StuffName, int[] Miktari, string[] Birim, HttpPostedFileBase[] ResimID, Kampanya ka, HttpPostedFileBase resim12)
        {
            Kampanya kal = ctx.Kampanyas.FirstOrDefault(x => x.KampanyaId == id);
            if (ka.Aciklama != " ")
            {
                kal.Aciklama = ka.Aciklama;

            }
            if (resim12 != null)
            {
                Image img = Image.FromStream(resim12.InputStream);
                int width = Convert.ToInt32(ConfigurationManager.AppSettings["kampKucukW"]);
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["kampKucukh"]);
                string name = "/Content/Uploads/KucukFoto" + Guid.NewGuid() + Path.GetExtension(resim12.FileName);
                Bitmap bm = new Bitmap(img, width, height);
                bm.Save(Server.MapPath(name));
                kal.KampanyaFoto = name;
                int wd = Convert.ToInt32(ConfigurationManager.AppSettings["kampBuyukW"]);
                int hd = Convert.ToInt32(ConfigurationManager.AppSettings["kampBuyukH"]);
                string name2 = "/Content/Uploads/BuyukFoto" + Guid.NewGuid() + Path.GetExtension(resim12.FileName);
                Bitmap bm2 = new Bitmap(img, wd, hd);
                bm.Save(Server.MapPath(name2));
                kal.KampanyaFotoB = name2;
            }
            Stuff dat = ctx.Stuffs.FirstOrDefault(x => x.KampanyaID == id);
            if (StuffName[0] != null && Miktari[0] != 0 && Birim[0] != null)
            {


                for (int i = 0; i < StuffName.Count() && i < Birim.Count() && i < Miktari.Count(); i++)
                {
                    dat.Birim = Birim[i];
                    dat.Miktari = Miktari[i];
                    dat.StuffName = StuffName[i];
                    dat.KampanyaID = 0;
                    dat.KalanMiktar = 0;
                    if (dat.KampanyaID != -1)
                    {
                        dat.KampanyaID = id;
                    }
                    ctx.Stuffs.Add(dat);
                }
            }
            Resim rsm = new Resim();
            if (ResimID[0] != null)
            {
                for (int i = 0; i < ResimID.Count(); i++)
                {
                    string name = "/Content/Uploads/Olaylar/" + Guid.NewGuid() + Path.GetExtension(ResimID[i].FileName);
                    ResimID[i].SaveAs(Server.MapPath(name));
                    rsm.ResimBuyukYol = name;
                    rsm.ResimKucukYol = name;
                    rsm.KampanyaID = id;
                    ctx.Resims.Add(rsm);
                    try
                    {
                        ctx.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }
                }
            }
            ctx.SaveChanges();
            Guid idd = kal.Baslatan;
            return Redirect("/Kullanici/Index/" + idd);
        }

    }
}