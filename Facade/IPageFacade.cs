using System;
using Dto;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Facade
{
    [ServiceContract]
    public interface IPageFacade
    {
        [OperationContract]
        IList<PageDto> GetAllByParentIdPage(int parentId, bool? isActive);
        [OperationContract]
        IList<PageDto> GetLocalesByPageIdPage(int pageId);
        [OperationContract]
        List<PageDto> GetProductsPage(bool isDurer);
        [OperationContract]
        PageDto GetLocaleByIdPage(int id);
        [OperationContract]
        void UpdatePage(int id, string title, string content, string imageUrl);
        [OperationContract]
        PageDto GetByPermalinkPage(string permalink);
        [OperationContract]
        string GetOtherCulturePermalinkPage(string permalink);
        [OperationContract]
        PageDto GetFirstByParentIdPage(int parentId);
        [OperationContract]
        PageDto GetByIdPage(int id);
        [OperationContract]
        IList<PageDto> GetPageLinksByParentIdPage(int parentId, bool? isActive);
        [OperationContract]
        bool hasSubLinksPage(int parentId);
        [OperationContract]
        int GetMaxOrderPage(int parentId);
        [OperationContract]
        int GetMaxFileOrderPage(int pageId);
        [OperationContract]
        string RemoveAccentPage(string txt);
        [OperationContract]
        void CreatePage(int parentId, string title, string content, string imageUrl);
        [OperationContract]
        void UpdateActivePage(int pageId);
        [OperationContract]
        bool DeleteItemPage(int pageId);
        [OperationContract]
        bool UpdateOrderPage(int pageId, bool isDown);
        [OperationContract]
        IList<PageFileDto> GetFilesByIdPage(int pageId);
        [OperationContract]
        bool UpdateFileOrderPage(int fileId, bool isDown);
        [OperationContract]
        bool DeleteFilePage(int fileId);
        [OperationContract]
        void CreateFilePage(int pageId, string fileUrl, string title);
        [OperationContract]
        IList<PageFileDto> GetFileLocalesByIdPage(int fileId);
        [OperationContract]
        void UpdateFilePage(int id, string fileUrl, string title);
        [OperationContract]
        PageFileDto GetFileLocaleByIdPage(int id);
    }
}
