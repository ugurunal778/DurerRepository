using Dto;
using System;
using System.Collections.Generic;
using WcfServiceApp;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class WcfService : BaseWcfService, IWcfService
    {
        public bool CheckLoginUser(string userName, string pass)
        {
            return UserFacade.CheckLoginUser(userName, pass);
        }

        public void CreateContent(int catId, string title, string desc, string fileTitle, string fileUrl, string imageUrl) => ContentFacade.CreateContent(catId, title, desc, fileTitle, fileUrl, imageUrl);

        public void CreateFilePage(int pageId, string fileUrl, string title) => pageFacade.CreateFilePage(pageId, fileUrl, title);

        public void CreateImageNews(int newsId, string imageUrl) => NewsFacade.CreateImageNews(newsId, imageUrl);

        public void CreateNews(string title, string content, DateTime date) => NewsFacade.CreateNews(title, content, date);

        public void CreatePage(int parentId, string title, string content, string imageUrl) => pageFacade.CreatePage(parentId, title, content, imageUrl);

        public bool DeleteFilePage(int fileId)
        {
            var result = pageFacade.DeleteFilePage(fileId);
            return result;
        }

        public bool DeleteImageNews(int imageId)
        {
            var result = NewsFacade.DeleteImageNews(imageId);
            return result;
        }

        public bool DeleteItemContent(int contentId)
        {
            var result = ContentFacade.DeleteItemContent(contentId);
            return result;
        }

        public bool DeleteItemNews(int newsId)
        {
            var result = NewsFacade.DeleteItemNews(newsId);
            return result;
        }

        public bool DeleteItemPage(int pageId)
        {
            var result = pageFacade.DeleteItemPage(pageId);
            return result;
        }

        public IList<ContentDto> GetAllByCategoryIdContent(int catId, bool? isActive)
        {
            var result = ContentFacade.GetAllByCategoryIdContent(catId, isActive);
            return result;
        }

        public IList<PageDto> GetAllByParentIdPage(int parentId, bool? isActive)
        {
            var result = pageFacade.GetAllByParentIdPage(parentId, isActive);
            return result;
        }

        public IList<NewsImageDto> GetAllImagesNews()
        {
            var result = NewsFacade.GetAllImagesNews();
            return result;
        }

        public IList<NewsDto> GetAllNews(bool? isActive)
        {
            var result = NewsFacade.GetAllNews(isActive);
            return result;
        }

        public ContentBannerDto GetBannerLocaleByContentIdContent(int id)
        {
            var result = ContentFacade.GetBannerLocaleByContentIdContent(id);
            return result;
        }

        public ContentBannerDto GetBannerLocaleByIdContent(int id)
        {
            var result = ContentFacade.GetBannerLocaleByIdContent(id);
            return result;
        }

        public IList<ContentBannerDto> GetBannerLocalesByIdContent(int id)
        {
            var result = ContentFacade.GetBannerLocalesByIdContent(id);
            return result;
        }

        public NewsDto GetByIdNews(int id, bool? isActive)
        {
            var result = NewsFacade.GetByIdNews(id, isActive);
            return result;
        }

        public PageDto GetByIdPage(int id)
        {
            var result = pageFacade.GetByIdPage(id);
            return result;
        }

        public PageDto GetByPermalinkPage(string permalink)
        {
            var result = pageFacade.GetByPermalinkPage(permalink);
            return result;
        }

        public PageFileDto GetFileLocaleByIdPage(int id)
        {
            var result = pageFacade.GetFileLocaleByIdPage(id);
            return result;
        }

        public IList<PageFileDto> GetFileLocalesByIdPage(int fileId)
        {
            var result = pageFacade.GetFileLocalesByIdPage(fileId);
            return result;
        }

        public IList<PageFileDto> GetFilesByIdPage(int pageId)
        {
            var result = pageFacade.GetFilesByIdPage(pageId);
            return result;
        }

        public PageDto GetFirstByParentIdPage(int parentId)
        {
            var result = pageFacade.GetFirstByParentIdPage(parentId);
            return result;
        }

        public NewsImageDto GetImageByIdNews(int id)
        {
            var result = NewsFacade.GetImageByIdNews(id);
            return result;
        }

        public IList<NewsImageDto> GetImagesByIdNews(int id)
        {
            var result = NewsFacade.GetImagesByIdNews(id);
            return result;
        }

        public ContentDto GetLocaleByIdContent(int id)
        {
            var result = ContentFacade.GetLocaleByIdContent(id);
            return result;
        }

        public NewsDto GetLocaleByIdNews(int id)
        {
            var result = NewsFacade.GetLocaleByIdNews(id);
            return result;
        }

        public PageDto GetLocaleByIdPage(int id)
        {
            var result = pageFacade.GetLocaleByIdPage(id);
            return result;
        }

        public IList<ContentDto> GetLocalesByContentIdContent(int contentId)
        {
            var result = ContentFacade.GetLocalesByContentIdContent(contentId);
            return result;
        }

        public IList<NewsDto> GetLocalesByIdNews(int id)
        {
            var result = NewsFacade.GetLocalesByIdNews(id);
            return result;
        }

        public IList<PageDto> GetLocalesByPageIdPage(int pageId)
        {
            var result = pageFacade.GetLocalesByPageIdPage(pageId);
            return result;
        }

        public int GetMaxFileOrderPage(int pageId)
        {
            var result = pageFacade.GetMaxFileOrderPage(pageId);
            return result;
        }

        public int GetMaxOrderContent(int catId)
        {
            var result = ContentFacade.GetMaxOrderContent(catId);
            return result;
        }

        public int GetMaxOrderNews()
        {
            var result = NewsFacade.GetMaxOrderNews();
            return result;
        }

        public int GetMaxOrderPage(int parentId)
        {
            var result = pageFacade.GetMaxOrderPage(parentId);
            return result;
        }

        public string GetOtherCulturePermalinkPage(string permalink)
        {
            var result = pageFacade.GetOtherCulturePermalinkPage(permalink);
            return result;
        }

        public IList<PageDto> GetPageLinksByParentIdPage(int parentId, bool? isActive)
        {
            var result = pageFacade.GetPageLinksByParentIdPage(parentId, isActive);
            return result;
        }

        public List<PageDto> GetProductsPage(bool isDurer)
        {
            var result = pageFacade.GetProductsPage(isDurer);
            return result;
        }

        public bool hasSubLinksPage(int parentId)
        {
            var result = pageFacade.hasSubLinksPage(parentId);
            return result;
        }

        public string RemoveAccentPage(string txt)
        {
            var result = pageFacade.RemoveAccentPage(txt);
            return result;
        }

        public void SendMail(string body, string fullName, string title) => MailFacade.SendMail(body, fullName, title);

        public void UpdateActiveContent(int contentId) => ContentFacade.UpdateActiveContent(contentId);

        public void UpdateActiveNews(int newsId) => NewsFacade.UpdateActiveNews(newsId);

        public void UpdateActivePage(int pageId) => pageFacade.UpdateActivePage(pageId);

        public void UpdateBannerContent(int id, string imageUrl) => ContentFacade.UpdateBannerContent(id, imageUrl);

        public void UpdateContent(int id, string title, string content, string fileTitle, string fileUrl, string imageUrl) => ContentFacade.UpdateContent(id, title, content, fileTitle, fileUrl, imageUrl);

        public bool UpdateFileOrderPage(int fileId, bool isDown) => pageFacade.UpdateFileOrderPage(fileId, isDown);

        public void UpdateFilePage(int id, string fileUrl, string title) => pageFacade.UpdateFilePage(id, fileUrl, title);

        public void UpdateImageNews(int id, string imageUrl) => NewsFacade.UpdateImageNews(id, imageUrl);

        public void UpdateNews(int id, string title, string content, DateTime date) => NewsFacade.UpdateNews(id, title, content, date);

        public bool UpdateOrderContent(int contentId, bool isDown) => ContentFacade.UpdateOrderContent(contentId, isDown);

        public bool UpdateOrderNews(int newsId, bool isDown) => NewsFacade.UpdateOrderNews(newsId, isDown);

        public bool UpdateOrderPage(int pageId, bool isDown) => pageFacade.UpdateOrderPage(pageId, isDown);

        public void UpdatePage(int id, string title, string content, string imageUrl) => pageFacade.UpdatePage(id, title, content, imageUrl);
    }
}
