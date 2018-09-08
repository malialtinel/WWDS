using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WWD.Models;
namespace WWD.Controllers
{
    public class YonetimController : Controller
    {
        DonationDB ctx = new DonationDB();
        // GET: Yonetim

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Giris()
        {
            return View();
        }
        public ActionResult Kayit()
        {
            return View();
        }
        public ActionResult Reset()
        {
            return View();
        }
        //Login çalışıyor
        [HttpPost]
        public ActionResult Giris(Kullanici kl, string rememberMe)
        {
            bool sonuc = Membership.ValidateUser(kl.UserAdi, kl.Parola);
            if (sonuc)
            {
                if (rememberMe == "on")
                {
                    FormsAuthentication.RedirectFromLoginPage(kl.UserAdi, true);
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(kl.UserAdi, false);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.mesaj = "Password or Username is not correct";
                return View("Giris");
            }

        }
        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Resetle(Kullanici kl)
        {
            MembershipUser mu = Membership.GetUser(kl.UserAdi);
            if (mu.PasswordQuestion == kl.GizliSoru)
            {
                string pwd = mu.ResetPassword(kl.GizliCevap);
                mu.ChangePassword(pwd, kl.Parola);
                return RedirectToAction("Giris");
            }
            else
            {
                ViewBag.mesaj = "Incorrect Informations";
                return View();
            }


        }
        
        [HttpPost]
        public ActionResult Kayit(Kullanici kl)
        {
            MembershipCreateStatus durum;
            MembershipUser ASS = Membership.CreateUser(kl.UserAdi, kl.Parola, kl.Email, kl.GizliSoru, kl.GizliCevap, true, out durum);
            string mesaj = " ";
            switch (durum)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj += "Invalid User Name";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj += "Invalid Password";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj += "Invalid Secret Question";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj += "Invalid Secret Answer";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj += "Invalid E-mail";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj += "Already This User Name Exist";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj += "Already This E-mail Exist";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj += "Rejected";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    break;
                case MembershipCreateStatus.ProviderError:
                    break;
                default:
                    break;
            }
            ViewBag.mesaj = mesaj;
            if (durum == MembershipCreateStatus.Success)
            {
                
                Guid id = (Guid)Membership.GetUser(kl.UserAdi).ProviderUserKey;
                Roles.AddUserToRole(kl.UserAdi, "UYE");
                kl.ProfilFoto = null;
                kl.IsAdmin = false;
                kl.IsDonator = false;
                kl.IsLeader = false;
                kl.KullaniciId = id;
                ctx.Kullanicis.Add(kl);
                ctx.SaveChanges();
                return RedirectToAction("Giris", "Yonetim");
            }
            return View();
        }
        [Authorize(Roles = "Boss")]
        public ActionResult RolEkle()
        {
            ViewBag.rol = Roles.GetAllRoles().ToList();
            ViewBag.admin = Membership.GetAllUsers();
            return View();
        }
        [Authorize(Roles = "Boss")]
        [HttpPost]
        public ActionResult RolEle(string roladi)
        {
            Roles.CreateRole(roladi);
            return View("RolEkle");
        }
        [Authorize(Roles = "Boss")]
        [HttpPost]
        public ActionResult RolAta(string KullaniciAdi, string RolAdi)
        {
            Roles.AddUserToRole(KullaniciAdi, RolAdi);
            return View("RolEkle");
        }
        [Authorize(Roles = "Boss")]
        public ActionResult Bekleyenler()
        {
            ViewBag.bekleyenler = ctx.Kampanyas.Where(x => x.OnaylandiMi == false).ToList();
            return View();
        }
        [Authorize(Roles = "Boss")]
        public ActionResult DevamEdenler()
        {
            ViewBag.devamedenler = ctx.Kampanyas.Where(x => x.OnaylandiMi == true && x.BasariliMi == false).ToList();
            return View();
        }
        [Authorize(Roles = "Boss")]
        public ActionResult Bitenler()
        {
            ViewBag.bitenler = ctx.Kampanyas.Where(x => x.BasariliMi == true).ToList();
            return View();
        }
        [Authorize(Roles = "Boss")]
        public ActionResult Onaylandi(int id)
        {
         
            Kampanya ka = ctx.Kampanyas.FirstOrDefault(x => x.KampanyaId == id);
            ka.OnaylandiMi =true;
            ctx.SaveChanges();
            return RedirectToAction("Bekleyenler");

        }
        [Authorize(Roles = "Boss")]
        public ActionResult Rededildi(int id)
        {
           List<Stuff> st = ctx.Stuffs.Where(x => x.KampanyaID == id).ToList();
            List<Donate> dn = ctx.Donates.Where(x => x.KampanyaID == id).ToList();
            Kampanya ka = ctx.Kampanyas.FirstOrDefault(x => x.KampanyaId == id);
            if (ka!=null)
            {
                for (int i = 0; i < st.Count(); i++)
                {
                    ctx.Stuffs.Remove(st[i]);
                   
                    
                    ctx.SaveChanges();
                }
                if (dn!=null)
                {
                    for (int i = 0; i < dn.Count(); i++)
                    {
                        ctx.Donates.Remove(dn[i]);


                        ctx.SaveChanges();
                    }
                }

                ctx.Kampanyas.Remove(ka);
                ctx.SaveChanges();
                return RedirectToAction("Bekleyenler");
            }
            
          
            return RedirectToAction("Bekleyenler");

        }
    }
}