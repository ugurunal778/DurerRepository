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
    public interface IContentFacade
    {
        [OperationContract]
        IList<ContentDto> GetAllByCategoryIdContent(int catId, bool? isActive);
        [OperationContract]
        void UpdateActiveContent(int contentId);
        [OperationContract]
        bool DeleteItemContent(int contentId);
        [OperationContract]
        bool UpdateOrderContent(int contentId, bool isDown);
        [OperationContract]
        void CreateContent(int catId, string title, string desc, string fileTitle, string fileUrl, string imageUrl);
        [OperationContract]
        int GetMaxOrderContent(int catId);
        [OperationContract]
        IList<ContentDto> GetLocalesByContentIdContent(int contentId);
        [OperationContract]
        void UpdateContent(int id, string title, string content, string fileTitle, string fileUrl, string imageUrl);
        [OperationContract]
        ContentDto GetLocaleByIdContent(int id);
        [OperationContract]
        ContentBannerDto GetBannerLocaleByIdContent(int id);
        [OperationContract]
        ContentBannerDto GetBannerLocaleByContentIdContent(int id);
        [OperationContract]
        IList<ContentBannerDto> GetBannerLocalesByIdContent(int id);
        [OperationContract]
        void UpdateBannerContent(int id, string imageUrl);
    }
}
