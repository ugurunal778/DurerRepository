using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using durer2.Models;

namespace durer2.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            if (!ProjectService.CheckLoginUser(user, pass))
            {
                ViewData["error"] = "Yanlış kullanıcı/şifre";
                return View();
            }
            var userCookie = new HttpCookie("User") { Value = user, Expires = DateTime.Now.AddMinutes(1200) };
            pass = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(pass));
            var passCookie = new HttpCookie("Pass") { Value = pass, Expires = DateTime.Now.AddMinutes(1200) };
            Response.Cookies.Add(userCookie);
            Response.Cookies.Add(passCookie);
            return RedirectToAction("Pages", new { id = 28 });
        }

        public ActionResult Logout()
        {
            var userCookie = Response.Cookies["User"];
            var passCookie = Response.Cookies["Pass"];
            userCookie.Value = null;
            passCookie.Value = null;
            return RedirectToAction("Login");
        }

        [UserAuthorize]
        public ActionResult Pages(int id)
        {
            return View(ProjectService.GetAllByParentIdPage(id, null));
        }

        [UserAuthorize]
        public ActionResult PageList(int id)
        {
            if (ProjectService.hasSubLinksPage(id))
            {
                var subPages = ProjectService.GetPageLinksByParentIdPage(id, null);
                ViewBag.subPage = true;
                return View(subPages);
            }
            ViewBag.pageFiles = ProjectService.GetFilesByIdPage(id);
            return View(ProjectService.GetLocalesByPageIdPage(id));
        }

        [UserAuthorize]
        public ActionResult PageEdit(int id)
        {
            var locale = ProjectService.GetLocaleByIdPage(id);
            return View(locale);
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PageEdit([Bind(Include = "Title, Content, LocaleId")] ServiceReference.PageDto page, HttpPostedFileBase ImageUrl)
        {
            string imgUrl = null;
            if (ImageUrl != null)
            {
                imgUrl = UploadImage(ImageUrl);
            }
            ProjectService.UpdatePage(page.LocaleId, page.Title, page.Content, imgUrl);
            ViewData["Success"] = "Kaydedildi";
            return RedirectToAction("PageEdit", new { id = page.LocaleId });
        }

        [UserAuthorize]
        public ActionResult PageAdd(int id)
        {
            ViewBag.parentPages = ProjectService.GetAllByParentIdPage(id, null);
            return View();
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PageAdd(int id, [Bind(Include = "ParentId, Title, Content")] ServiceReference.PageDto page, HttpPostedFileBase ImageUrl)
        {
            string imgUrl = null;
            if (ImageUrl != null)
            {
                imgUrl = UploadImage(ImageUrl);
            }
            if (page.ParentId == 0)
            {
                page.ParentId = id;
            }
            ProjectService.CreatePage(page.ParentId, page.Title, page.Content, imgUrl);
            ViewData["Success"] = "Eklendi";
            ViewBag.parentPages = ProjectService.GetAllByParentIdPage(id, null);
            return View();
        }

        [UserAuthorize]
        public ActionResult PageActive(int id)
        {
            ProjectService.UpdateActivePage(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult PageDelete(int id)
        {
            var page = ProjectService.DeleteItemPage(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult PageOrder(int id, bool isDown)
        {
            var news = ProjectService.UpdateOrderPage(id, isDown);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult News()
        {
            var news = ProjectService.GetAllNews(null);
            return View(news);
        }

        [UserAuthorize]
        public ActionResult News2()
        {
            var news = ProjectService.GetAllNews(null);
            return View(news);
        }

        [UserAuthorize]
        public ActionResult NewsActive(int id)
        {
            ProjectService.UpdateActiveNews(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult NewsOrder(int id, bool isDown)
        {
            var news = ProjectService.UpdateOrderNews(id, isDown);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult NewsDelete(int id)
        {
            var news = ProjectService.DeleteItemNews(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult NewsList(int id)
        {
            var locales = ProjectService.GetLocalesByIdNews(id);
            ViewBag.images = ProjectService.GetImagesByIdNews(id);
            return View(locales);
        }

        [UserAuthorize]
        public ActionResult NewsEdit(int id)
        {
            var locale = ProjectService.GetLocaleByIdNews(id);
            return View(locale);
        }

        [HttpPost]
        [ValidateInput(false)]
        [UserAuthorize]
        public ActionResult NewsEdit([Bind(Include = "Title, Content, LocaleId")] ServiceReference.NewsDto news, string CreateDate)
        {
            if (news.Title != null)
            {
                DateTime date = DateTime.ParseExact(CreateDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ProjectService.UpdateNews(news.LocaleId, news.Title, news.Content, date);
                ViewData["Success"] = "Kaydedildi";
            }
            else
            {
                ViewData["Error"] = "Lüfen boş bırakmayın";
            }
            return RedirectToAction("NewsEdit", new { id = news.LocaleId });
        }

        [UserAuthorize]
        public ActionResult NewsAdd()
        {
            return View();
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewsAdd([Bind(Include = "Title, Content")] ServiceReference.NewsDto news, string CreateDate)
        {
            DateTime date = DateTime.Parse(CreateDate);
            ProjectService.CreateNews(news.Title, news.Content, date);
            ViewData["Success"] = "Eklendi";
            return View(news);
        }

        [UserAuthorize]
        public ActionResult NewsAddImage(int id)
        {
            return View();
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewsAddImage(int id, HttpPostedFileBase ImageUrl)
        {
            string imgUrl = null;
            if (ImageUrl != null)
            {
                imgUrl = UploadImage(ImageUrl);
            }
            ProjectService.CreateImageNews(id, imgUrl);
            ViewData["Success"] = "Eklendi";
            return View();
        }

        [UserAuthorize]
        public ActionResult NewsDeleteImage(int id)
        {
            var news = ProjectService.DeleteImageNews(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult NewsEditImage(int id)
        {
            var image = ProjectService.GetImageByIdNews(id);
            return View(image);
        }

        [HttpPost]
        [ValidateInput(false)]
        [UserAuthorize]
        public ActionResult NewsEditImage(int id, HttpPostedFileBase ImageUrl)
        {
            string imgUrl = null;
            if (ImageUrl != null)
            {
                imgUrl = UploadImage(ImageUrl);
            }
            ProjectService.UpdateImageNews(id, imgUrl);
            ViewData["Success"] = "Güncellendi";
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult PageOrderFile(int id, bool isDown)
        {
            var news = ProjectService.UpdateFileOrderPage(id, isDown);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult PageDeleteFile(int id)
        {
            var news = ProjectService.DeleteFilePage(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult PageAddFile(int id)
        {
            return View();
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PageAddFile(int id, HttpPostedFileBase FileUrl, string Title)
        {
            string flUrl = null;
            if (FileUrl != null)
            {
                flUrl = UploadImage(FileUrl);
            }
            ProjectService.CreateFilePage(id, flUrl, Title);
            ViewData["Success"] = "Eklendi";
            return View();
        }

        [UserAuthorize]
        public ActionResult PageFileList(int id)
        {
            return View(ProjectService.GetFileLocalesByIdPage(id));
        }

        [UserAuthorize]
        public ActionResult PageEditFile(int id)
        {
            var file = ProjectService.GetFileLocaleByIdPage(id);
            return View(file);
        }

        [HttpPost]
        [ValidateInput(false)]
        [UserAuthorize]
        public ActionResult PageEditFile(int id, HttpPostedFileBase FileUrl, string Title)
        {
            string flUrl = null;
            if (FileUrl != null)
            {
                flUrl = UploadImage(FileUrl);
            }
            ProjectService.UpdateFilePage(id, flUrl, Title);
            ViewData["Success"] = "Güncellendi";
            return Redirect(Request.UrlReferrer.ToString());
        }


        [UserAuthorize]
        public ActionResult MediaList()
        {
            var mediaList = ProjectService.GetAllByCategoryIdContent(1, null);
            return View(mediaList);
        }

        [UserAuthorize]
        public ActionResult MediaActive(int id)
        {
            ProjectService.UpdateActiveContent(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult MediaDelete(int id)
        {
            var content = ProjectService.DeleteItemContent(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult MediaOrder(int id, bool isDown)
        {
            var news = ProjectService.UpdateOrderContent(id, isDown);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult MediaAdd()
        {
            return View();
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MediaAdd([Bind(Include = "CategoryId, Title, ImageUrl")] ServiceReference.ContentDto content, HttpPostedFileBase ImageUrl, HttpPostedFileBase FileUrl)
        {
            string imgUrl = null;
            if (ImageUrl != null)
            {
                imgUrl = UploadImage(ImageUrl);
            }
            string flUrl = null;
            if (FileUrl != null)
            {
                flUrl = UploadImage(FileUrl);
            }
            content.CategoryId = 1;
            ProjectService.CreateContent(content.CategoryId, content.Title, "", "", flUrl, imgUrl);
            ViewData["Success"] = "Eklendi";
            return View();
        }

        [UserAuthorize]
        public ActionResult Media(int id)
        {
            var mediaLocales = ProjectService.GetLocalesByContentIdContent(id);
            return View(mediaLocales);
        }
        [UserAuthorize]
        public ActionResult MediaEdit(int id)
        {
            var locale = ProjectService.GetLocaleByIdContent(id);
            return View(locale);
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MediaEdit(int id, [Bind(Include = "Title, LocaleId")] ServiceReference.ContentDto content, HttpPostedFileBase ImageUrl, HttpPostedFileBase FileUrl)
        {
            string imgUrl = null;
            if (ImageUrl != null)
            {
                imgUrl = UploadImage(ImageUrl);
            }
            string flUrl = null;
            if (FileUrl != null)
            {
                flUrl = UploadImage(FileUrl);
            }
            ProjectService.UpdateContent(content.LocaleId, content.Title, "", "", flUrl, imgUrl);
            ViewData["Success"] = "Kaydedildi";
            return RedirectToAction("MediaEdit", new { id = content.LocaleId });
        }

        [UserAuthorize]
        public ActionResult Catalogs()
        {
            var certificates = ProjectService.GetAllByCategoryIdContent(3, null);
            return View(certificates);
        }

        [UserAuthorize]
        public ActionResult CatalogActive(int id)
        {
            ProjectService.UpdateActiveContent(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult CatalogDelete(int id)
        {
            var content = ProjectService.DeleteItemContent(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult CatalogOrder(int id, bool isDown)
        {
            var news = ProjectService.UpdateOrderContent(id, isDown);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult CatalogAdd()
        {
            return View();
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CatalogAdd([Bind(Include = "CategoryId, FileTitle, FileUrl, ImageUrl")] ServiceReference.ContentDto content, HttpPostedFileBase FileUrl, HttpPostedFileBase ImageUrl)
        {
            string flUrl = null;
            if (FileUrl != null)
            {
                flUrl = UploadImage(FileUrl);
            }
            string imgUrl = null;
            if (ImageUrl != null)
            {
                imgUrl = UploadImage(ImageUrl);
            }
            content.CategoryId = 3;
            ProjectService.CreateContent(content.CategoryId, "", "", content.FileTitle, flUrl, imgUrl);
            ViewData["Success"] = "Eklendi";
            return View();
        }

        [UserAuthorize]
        public ActionResult Catalog(int id)
        {
            var mediaLocales = ProjectService.GetLocalesByContentIdContent(id);
            return View(mediaLocales);
        }

        [UserAuthorize]
        public ActionResult CatalogEdit(int id)
        {
            var locale = ProjectService.GetLocaleByIdContent(id);
            return View(locale);
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CatalogEdit(int id, [Bind(Include = "FileTitle, LocaleId")] ServiceReference.ContentDto content, HttpPostedFileBase FileUrl, HttpPostedFileBase ImageUrl)
        {
            string flUrl = null;
            if (FileUrl != null)
            {
                flUrl = UploadImage(FileUrl);
            }
            string imgUrl = null;
            if (ImageUrl != null)
            {
                imgUrl = UploadImage(ImageUrl);
            }
            ProjectService.UpdateContent(content.LocaleId, "", "", content.FileTitle, flUrl, imgUrl);
            ViewData["Success"] = "Kaydedildi";
            return RedirectToAction("CatalogEdit", new { id = content.LocaleId });
        }

        [UserAuthorize]
        public ActionResult BannerList(int id)
        {
            var locales = ProjectService.GetBannerLocalesByIdContent(id);
            return View(locales);
        }

        [UserAuthorize]
        public ActionResult BannerEdit(int id)
        {
            var locale = ProjectService.GetBannerLocaleByIdContent(id);
            return View(locale);
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BannerEdit(int id, HttpPostedFileBase ImageUrl)
        {
            string imgUrl = null;
            if (ImageUrl != null)
            {
                imgUrl = UploadImage(ImageUrl);
            }
            ProjectService.UpdateBannerContent(id, imgUrl);
            ViewData["Success"] = "Kaydedildi";
            return RedirectToAction("BannerEdit", new { id = id });
        }
    }
}