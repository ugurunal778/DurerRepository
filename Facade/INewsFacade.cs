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
    public interface INewsFacade
    {
        [OperationContract]
        IList<NewsDto> GetAllNews(bool? isActive);
        [OperationContract]
        IList<NewsImageDto> GetAllImagesNews();
        [OperationContract]
        void UpdateActiveNews(int newsId);
        [OperationContract]
        bool UpdateOrderNews(int newsId, bool isDown);
        [OperationContract]
        int GetMaxOrderNews();
        [OperationContract]
        bool DeleteItemNews(int newsId);
        [OperationContract]
        IList<NewsDto> GetLocalesByIdNews(int id);
        [OperationContract]
        NewsDto GetLocaleByIdNews(int id);
        [OperationContract]
        void UpdateNews(int id, string title, string content, DateTime date);
        [OperationContract]
        void CreateNews(string title, string content, DateTime date);
        [OperationContract]
        NewsDto GetByIdNews(int id, bool? isActive);
        [OperationContract]
        IList<NewsImageDto> GetImagesByIdNews(int id);
        [OperationContract]
        NewsImageDto GetImageByIdNews(int id);
        [OperationContract]
        void CreateImageNews(int newsId, string imageUrl);
        [OperationContract]
        bool DeleteImageNews(int imageId);
        [OperationContract]
        void UpdateImageNews(int id, string imageUrl);
    }
}
