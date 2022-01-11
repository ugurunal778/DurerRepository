using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

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
            if (!UserFacade.CheckLogin(user, pass))
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
            return View(PageFacade.GetAllByParentId(id, null));
        }

        [UserAuthorize]
        public ActionResult PageList(int id)
        {
            if (PageFacade.hasSubLinks(id))
            {
                var subPages = PageFacade.getPageLinksByParentId(id, null);
                ViewBag.subPage = true;
                return View(subPages);
            }
            ViewBag.pageFiles = PageFacade.GetFilesById(id);
            return View(PageFacade.GetLocalesByPageId(id));
        }

        [UserAuthorize]
        public ActionResult PageEdit(int id)
        {
            var locale = PageFacade.GetLocaleById(id);
            return View(locale);
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PageEdit([Bind(Include = "Title, Content, LocaleId")] Dto.PageDto page, HttpPostedFileBase ImageUrl)
        {
            string imgUrl = null;
            if (ImageUrl != null)
            {
                imgUrl = UploadImage(ImageUrl);
            }
            PageFacade.Update(page.LocaleId, page.Title, page.Content, imgUrl);
            ViewData["Success"] = "Kaydedildi";
            return RedirectToAction("PageEdit", new { id = page.LocaleId });
        }

        [UserAuthorize]
        public ActionResult PageAdd(int id)
        {
            ViewBag.parentPages = PageFacade.GetAllByParentId(id, null);
            return View();
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PageAdd(int id, [Bind(Include = "ParentId, Title, Content")] Dto.PageDto page, HttpPostedFileBase ImageUrl)
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
            PageFacade.Create(page.ParentId, page.Title, page.Content, imgUrl);
            ViewData["Success"] = "Eklendi";
            ViewBag.parentPages = PageFacade.GetAllByParentId(id, null);
            return View();
        }

        [UserAuthorize]
        public ActionResult PageActive(int id)
        {
            PageFacade.UpdateActive(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult PageDelete(int id)
        {
            var page = PageFacade.DeleteItem(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult PageOrder(int id, bool isDown)
        {
            var news = PageFacade.UpdateOrder(id, isDown);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult News()
        {
            var news = NewsFacade.GetAll(null);
            return View(news);
        }

        [UserAuthorize]
        public ActionResult NewsActive(int id)
        {
            NewsFacade.UpdateActive(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult NewsOrder(int id, bool isDown)
        {
            var news = NewsFacade.UpdateOrder(id, isDown);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult NewsDelete(int id)
        {
            var news = NewsFacade.DeleteItem(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult NewsList(int id)
        {
            var locales = NewsFacade.GetLocalesById(id);
            ViewBag.images = NewsFacade.GetImagesById(id);
            return View(locales);
        }

        [UserAuthorize]
        public ActionResult NewsEdit(int id)
        {
            var locale = NewsFacade.GetLocaleById(id);
            return View(locale);
        }

        [HttpPost]
        [ValidateInput(false)]
        [UserAuthorize]
        public ActionResult NewsEdit([Bind(Include = "Title, Content, LocaleId")] Dto.NewsDto news, string CreateDate)
        {
            if (news.Title != null)
            {
                DateTime date = DateTime.ParseExact(CreateDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                NewsFacade.Update(news.LocaleId, news.Title, news.Content, date);
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
        public ActionResult NewsAdd([Bind(Include = "Title, Content")] Dto.NewsDto news, string CreateDate)
        {
            DateTime date = DateTime.Parse(CreateDate);
            NewsFacade.Create(news.Title, news.Content, date);
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
            NewsFacade.CreateImage(id, imgUrl);
            ViewData["Success"] = "Eklendi";
            return View();
        }

        [UserAuthorize]
        public ActionResult NewsDeleteImage(int id)
        {
            var news = NewsFacade.DeleteImage(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult NewsEditImage(int id)
        {
            var image = NewsFacade.GetImageById(id);
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
            NewsFacade.UpdateImage(id, imgUrl);
            ViewData["Success"] = "Güncellendi";
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult PageOrderFile(int id, bool isDown)
        {
            var news = PageFacade.UpdateFileOrder(id, isDown);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult PageDeleteFile(int id)
        {
            var news = PageFacade.DeleteFile(id);
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
            PageFacade.CreateFile(id, flUrl, Title);
            ViewData["Success"] = "Eklendi";
            return View();
        }

        [UserAuthorize]
        public ActionResult PageFileList(int id)
        {
            return View(PageFacade.GetFileLocalesById(id));
        }

        [UserAuthorize]
        public ActionResult PageEditFile(int id)
        {
            var file = PageFacade.GetFileLocaleById(id);
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
            PageFacade.UpdateFile(id, flUrl, Title);
            ViewData["Success"] = "Güncellendi";
            return Redirect(Request.UrlReferrer.ToString());
        }


        [UserAuthorize]
        public ActionResult MediaList()
        {
            var mediaList = ContentFacade.GetAllByCategoryId(1, null);
            return View(mediaList);
        }

        [UserAuthorize]
        public ActionResult MediaActive(int id)
        {
            ContentFacade.UpdateActive(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult MediaDelete(int id)
        {
            var content = ContentFacade.DeleteItem(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult MediaOrder(int id, bool isDown)
        {
            var news = ContentFacade.UpdateOrder(id, isDown);
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
        public ActionResult MediaAdd([Bind(Include = "CategoryId, Title, ImageUrl")] Dto.ContentDto content, HttpPostedFileBase ImageUrl, HttpPostedFileBase FileUrl)
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
            ContentFacade.Create(content.CategoryId, content.Title, "", "", flUrl, imgUrl);
            ViewData["Success"] = "Eklendi";
            return View();
        }

        [UserAuthorize]
        public ActionResult Media(int id)
        {
            var mediaLocales = ContentFacade.GetLocalesByContentId(id);
            return View(mediaLocales);
        }
        [UserAuthorize]
        public ActionResult MediaEdit(int id)
        {
            var locale = ContentFacade.GetLocaleById(id);
            return View(locale);
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MediaEdit(int id, [Bind(Include = "Title, LocaleId")] Dto.ContentDto content, HttpPostedFileBase ImageUrl, HttpPostedFileBase FileUrl)
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
            ContentFacade.Update(content.LocaleId, content.Title, "", "", flUrl, imgUrl);
            ViewData["Success"] = "Kaydedildi";
            return RedirectToAction("MediaEdit", new { id = content.LocaleId });
        }

        [UserAuthorize]
        public ActionResult Catalogs()
        {
            var certificates = ContentFacade.GetAllByCategoryId(3, null);
            return View(certificates);
        }

        [UserAuthorize]
        public ActionResult CatalogActive(int id)
        {
            ContentFacade.UpdateActive(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult CatalogDelete(int id)
        {
            var content = ContentFacade.DeleteItem(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [UserAuthorize]
        public ActionResult CatalogOrder(int id, bool isDown)
        {
            var news = ContentFacade.UpdateOrder(id, isDown);
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
        public ActionResult CatalogAdd([Bind(Include = "CategoryId, FileTitle, FileUrl, ImageUrl")] Dto.ContentDto content, HttpPostedFileBase FileUrl, HttpPostedFileBase ImageUrl)
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
            ContentFacade.Create(content.CategoryId, "", "", content.FileTitle, flUrl, imgUrl);
            ViewData["Success"] = "Eklendi";
            return View();
        }

        [UserAuthorize]
        public ActionResult Catalog(int id)
        {
            var mediaLocales = ContentFacade.GetLocalesByContentId(id);
            return View(mediaLocales);
        }

        [UserAuthorize]
        public ActionResult CatalogEdit(int id)
        {
            var locale = ContentFacade.GetLocaleById(id);
            return View(locale);
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CatalogEdit(int id, [Bind(Include = "FileTitle, LocaleId")] Dto.ContentDto content, HttpPostedFileBase FileUrl, HttpPostedFileBase ImageUrl)
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
            ContentFacade.Update(content.LocaleId, "", "", content.FileTitle, flUrl, imgUrl);
            ViewData["Success"] = "Kaydedildi";
            return RedirectToAction("CatalogEdit", new { id = content.LocaleId });
        }

        [UserAuthorize]
        public ActionResult BannerList(int id)
        {
            var locales = ContentFacade.GetBannerLocalesById(id);
            return View(locales);
        }

        [UserAuthorize]
        public ActionResult BannerEdit(int id)
        {
            var locale = ContentFacade.GetBannerLocaleById(id);
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
            ContentFacade.UpdateBanner(id, imgUrl);
            ViewData["Success"] = "Kaydedildi";
            return RedirectToAction("BannerEdit", new { id = id });
        }
    }
}