using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WWD.App_classes
{
    public class Settings
    {
        public static Size KampanyaKucuk
        {
            get
            {
                Size sonuc = new Size();
                sonuc.Width = Convert.ToInt32(ConfigurationManager.AppSettings["kampKucukW"]);
                sonuc.Height = Convert.ToInt32(ConfigurationManager.AppSettings["kampKucukH"]);
                return sonuc;
            }
        }
        public static Size KampanyaBuyuk
        {

            get
            {
                Size sonuc = new Size();
                sonuc.Width = Convert.ToInt32(ConfigurationManager.AppSettings["kampBuyukW"]);
                sonuc.Height = Convert.ToInt32(ConfigurationManager.AppSettings["kampBuyukH"]);
                return sonuc;
            }
        }
        public static Size ProfilFotoNormal
        {
            get
            {
                Size sonuc = new Size();
                sonuc.Width = Convert.ToInt32(ConfigurationManager.AppSettings["profilW"]);
                sonuc.Height = Convert.ToInt32(ConfigurationManager.AppSettings["profilH"]);
                return sonuc;
            }
        }
    }
}