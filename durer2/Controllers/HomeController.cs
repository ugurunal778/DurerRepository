using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace durer2.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Page(string permalink, bool? isSub)
        {
            var page = PageFacade.GetByPermalink(permalink);

            if (page == null)
            {
                string newPerma = PageFacade.GetOtherCulturePermalink(permalink);
                if (isSub == true)
                {
                    return RedirectToAction("Page", new { permalink = newPerma, isSub = true });
                }
                else
                {
                    return RedirectToAction("Page", new { permalink = newPerma });
                }
            }
            ViewBag.pageFiles = PageFacade.GetFilesById(page.Id);
            if (PageFacade.hasSubLinks(page.Id))
            {
                page = PageFacade.GetFirstByParentId(page.Id);
                return RedirectToAction("Page", new { permalink = page.Permalink, isSub = true });
            }
            if (isSub == true)
            {
                ViewBag.pageSubLinks = PageFacade.getPageLinksByParentId(page.ParentId, true);
                var parentPage = PageFacade.GetById(page.ParentId);
                ViewBag.pageLinks = PageFacade.getPageLinksByParentId(parentPage.ParentId, true);
                ViewBag.permalink = parentPage.Permalink;
                ViewBag.parentId = parentPage.ParentId;
                var hazrefProducts = PageFacade.GetProducts(false);
                var durerProducts = PageFacade.GetProducts(true);
                var hazrefProductsExists = hazrefProducts.Any(x => x.Permalink == permalink);
                var durerProductsExists = durerProducts.Any(x => x.Permalink == permalink);

                ViewBag.HazrefExist = hazrefProductsExists;
                ViewBag.DurerExist = durerProductsExists;

                ViewBag.HazrefProducts = hazrefProducts;
                ViewBag.DurerProducts = durerProducts;
                return View(page);
            }
            ViewBag.pageLinks = PageFacade.getPageLinksByParentId(page.ParentId, true);
            ViewBag.parentId = 0;
            return View(page);
        }

        public ActionResult iletisim()
        {
            if (CultureInfo.CurrentUICulture.ToString() == "en-US")
            {
                return RedirectToAction("contact");
            }
            return View();
        }

        public ActionResult contact()
        {
            if (CultureInfo.CurrentUICulture.ToString() == "tr-TR")
            {
                return RedirectToAction("iletisim");
            }
            return View();
        }


        public ActionResult basvuru()
        {
            return View();
        }

        [HttpPost]
        public ActionResult basvuru(string FullName, string Birth, string Marital, string Military, string Address,
                            string Tel, string School, string Department, string Graduation, string Lang1Name,
                            string Lang2Name, string Lang3Name, string lang1, string lang2, string lang3,
                            string Firm1, string Firm2, string Firm3, string Business1, string Business2, string Business3,
                            string Date1, string Date2, string Date3, string Job1, string Job2, string Job3,
                            string Reason1, string Reason2, string Reason3, string RefName1, string RefName2, string RefName3,
                            string RefAddress1, string RefAddress2, string RefAddress3, string RefTel1, string RefTel2,
                            string RefTel3, string Request, string Wage, string Height, string Weight, string Size,
                            string ShoeSize)
        {
            string body = @"<b>Ad/Soyad:</b>" + FullName + @" <br />
                        <b>Doğum Yeri ve Yılı:</b> " + Birth + @" <br />
                        <b>Medeni Hali: </b> " + Marital + @" <br />
                        <b>Askerlik Durumu:</b> " + Military + @" <br />
                        <b>İkametgah Adresi:</b> " + Address + @" <br />
                        <b>Tel No:</b> " + Tel + @" <br />
                        <b>Mezun Olduğu Okul:</b> " + School + @" <br />
                        <b>Fakülte/Bölüm:</b> " + Department + @" <br />
                        <b>Mezuniyet Tarihi:</b> " + Graduation + @" <br />
                        <b>Yabancı Dil 1:</b> " + Lang1Name + "(" + lang1 + @") <br />
                        <b>Yabancı Dil 2:</b> " + Lang2Name + "(" + lang2 + @") <br />
                        <b>Yabancı Dil 3:</b> " + Lang3Name + "(" + lang3 + @") <br />
                        <b>Önceki Çalışma Alanı 1:</b> " + Firm1 + " - " + Business1 + " - " + Date1 + " - " + Job1 + " - " + Reason1 + @" <br />
                        <b>Önceki Çalışma Alanı 2:</b> " + Firm2 + " - " + Business2 + " - " + Date2 + " - " + Job2 + " - " + Reason2 + @" <br />
                        <b>Önceki Çalışma Alanı 3:</b> " + Firm3 + " - " + Business3 + " - " + Date3 + " - " + Job3 + " - " + Reason3 + @" <br />
                        <b>Referans 1:</b> " + RefName1 + " - " + RefAddress1 + " - " + RefTel1 + @" <br />
                        <b>Referans 2:</b> " + RefName2 + " - " + RefAddress2 + " - " + RefTel2 + @" <br />
                        <b>Referans 3:</b> " + RefName3 + " - " + RefAddress3 + " - " + RefTel3 + @" <br />
                        <b>Görev Almak İstenilen Bölüm:</b> " + Request + @"  <br />
                        <b>İstenilen Ücret (net):</b> " + Wage + @"  <br />
                        <b>Boy (cm):</b> " + Height + @"  <br />
                        <b>Ağırlık (kg):</b> " + Weight + @"  <br />
                        <b>Beden:</b> " + Size + @"  <br />
                        <b>Ayakkabı Numarası:</b> " + ShoeSize + @"  <br />";
            MailFacade.SendMail(body, FullName, "Başvuru Formu");
            ViewData["Success"] = "Mesajınız Gönderildi.";
            return View();
        }


        public ActionResult msds()
        {
            return View();
        }

        [HttpPost]
        public ActionResult msds(string FullName, string Title, string Company, string Business, string Address,
                                string City, string Country, string ZipCode, string Phone, string Message)
        {
            string body = @"<b>Sorumlu Kişi:</b>" + FullName + @" <br />
                        <b>Unvan:</b> " + Title + @" <br />
                        <b>Şirket: </b> " + Company + @" <br />
                        <b>Sektör:</b> " + Business + @" <br />
                        <b>Adres:</b> " + Address + @" <br />
                        <b>Şehir/Ülke/Posta Kodu:</b> " + City + " / " + Country + " / " + ZipCode + @" <br />
                        <b>Telefon:</b> " + Phone + @"  <br />
                        <b>Mesaj:</b> " + Message + " <br />";
            MailFacade.SendMail(body, FullName, "Ürün Reçetesi İstek Formu");
            ViewData["Success"] = "Mesajınız Gönderildi.";
            return View();
        }

        public ActionResult supplier()
        {
            return View();
        }

        [HttpPost]
        public ActionResult supplier(string CompanyName, string Address, string City, string ZipCode,
                                    string Country, string Telephone, string Fax, string Homepage,
                                    string EmailComp, string ContactPerson, string Title, string Email, string Message)
        {
            string body = @"<b>Şirket:</b>" + CompanyName + @" <br />
                        <b>Adres:</b> " + Address + @" <br />
                        <b>Şehir/Ülke/Posta Kodu:</b> " + City + " / " + Country + " / " + ZipCode + @" <br />
                        <b>Telefon/Fax:</b> " + Telephone + " / " + Fax + @" <br />
                        <b>Websitesi/E-Posta</b> " + Homepage + " / " + EmailComp + @" <br />
                        <b>Sorumlu Kişi:</b> " + ContactPerson + @" <br />
                        <b>Unvan:</b> " + Title + @"  <br />
                        <b>E-Posta</b> " + Email + @" <br />
                        <b>Mesaj:</b> " + Message + " <br />";
            MailFacade.SendMail(body, ContactPerson, "Tedarikçi Kayıt Formu");
            ViewData["Success"] = "Mesajınız Gönderildi.";
            return View();
        }

        public ActionResult basin()
        {
            if (CultureInfo.CurrentUICulture.ToString() == "en-US")
            {
                return RedirectToAction("press");
            }
            ViewBag.pageLinks = PageFacade.getPageLinksByParentId(28, true);
            ViewBag.Banner = ContentFacade.GetBannerLocaleByContentId(1).ImageUrl;
            var press = ContentFacade.GetAllByCategoryId(1, true);
            return View(press);
        }

        public ActionResult press()
        {
            if (CultureInfo.CurrentUICulture.ToString() == "tr-TR")
            {
                return RedirectToAction("basin");
            }
            ViewBag.pageLinks = PageFacade.getPageLinksByParentId(28, true);
            ViewBag.Banner = ContentFacade.GetBannerLocaleByContentId(1).ImageUrl;
            var press = ContentFacade.GetAllByCategoryId(1, true);
            return View(press);
        }

        public ActionResult katalog()
        {
            if (CultureInfo.CurrentUICulture.ToString() == "en-US")
            {
                return RedirectToAction("catalog");
            }
            ViewBag.pageLinks = PageFacade.getPageLinksByParentId(30, true);
            ViewBag.Banner = ContentFacade.GetBannerLocaleByContentId(3).ImageUrl;
            var catalogs = ContentFacade.GetAllByCategoryId(3, true);
            return View(catalogs);
        }

        public ActionResult catalog()
        {
            if (CultureInfo.CurrentUICulture.ToString() == "tr-TR")
            {
                return RedirectToAction("katalog");
            }
            ViewBag.pageLinks = PageFacade.getPageLinksByParentId(30, true);
            ViewBag.Banner = ContentFacade.GetBannerLocaleByContentId(3).ImageUrl;
            var catalogs = ContentFacade.GetAllByCategoryId(3, true);
            return View(catalogs);
        }

        public ActionResult hizmetler()
        {
            if (CultureInfo.CurrentUICulture.ToString() == "en-US")
            {
                return RedirectToAction("services");
            }
            ViewBag.pageLinks = PageFacade.getPageLinksByParentId(31, true);
            ViewBag.Banner = ContentFacade.GetBannerLocaleByContentId(3).ImageUrl;
            return View();
        }

        public ActionResult services()
        {
            if (CultureInfo.CurrentUICulture.ToString() == "tr-TR")
            {
                return RedirectToAction("hizmetler");
            }
            ViewBag.pageLinks = PageFacade.getPageLinksByParentId(31, true);
            ViewBag.Banner = ContentFacade.GetBannerLocaleByContentId(3).ImageUrl;
            return View();
        }

        public ActionResult haberler()
        {
            if (CultureInfo.CurrentUICulture.ToString() == "en-US")
            {
                return RedirectToAction("news");
            }
            ViewBag.pageLinks = PageFacade.getPageLinksByParentId(28, true);
            ViewBag.Banner = ContentFacade.GetBannerLocaleByContentId(4).ImageUrl;
            ViewBag.News = NewsFacade.GetAll(true);
            var images = NewsFacade.GetAllImages();
            return View(images);
        }

        public ActionResult news()
        {
            if (CultureInfo.CurrentUICulture.ToString() == "tr-TR")
            {
                return RedirectToAction("haberler");
            }
            ViewBag.pageLinks = PageFacade.getPageLinksByParentId(28, true);
            ViewBag.Banner = ContentFacade.GetBannerLocaleByContentId(4).ImageUrl;
            ViewBag.News = NewsFacade.GetAll(true);
            var images = NewsFacade.GetAllImages();
            return View(images);
        }

        public ActionResult SetCulture(string cultureInfo)
        {
            var cookie = new HttpCookie("CultureInfo")
            {
                Value = cultureInfo
            };
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [ChildActionOnly]
        public ActionResult Navbar()
        {
            ViewBag.Nav1 = PageFacade.GetAllByParentId(28, true);
            ViewBag.Nav2 = PageFacade.GetAllByParentId(29, true);
            ViewBag.Nav3 = PageFacade.GetAllByParentId(30, true);
            ViewBag.Nav4 = PageFacade.GetAllByParentId(31, true);
            //ViewBag.Nav5 = PageFacade.GetAllByParentId(32, true);
            ViewBag.Nav6 = PageFacade.GetAllByParentId(33, true);
            return PartialView();
        }
    }
}