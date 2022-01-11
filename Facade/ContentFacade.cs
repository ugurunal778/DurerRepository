using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    public class ContentFacade:FacadeBase
    {
        public IList<ContentDto> GetAllByCategoryId(int catId, bool? isActive)
        {
            var data = (from p in EntityModel.Content
                        join q in EntityModel.ContentLocale on p.Id equals q.ContentId
                        where q.Lang == Culture && p.CategoryId==catId
                        orderby p.Order
                        select new ContentDto()
                        {
                            Id = p.Id,
                            Title = q.Title,
                            Content = q.Content,
                            FileTitle = q.FileTitle,
                            FileUrl = q.FileUrl,
                            ImageUrl = q.ImageUrl,
                            Active = p.Active,
                            Order = p.Order
                        });
            if (isActive.HasValue)
            {
                data=data.Where(x=>x.Active==isActive);
            }
            return data.ToList();
        }

        public void UpdateActive(int contentId)
        {
            var item = EntityModel.Content.FirstOrDefault(x => x.Id == contentId);
            if (item != null) item.Active = !item.Active;
            EntityModel.SaveChanges();
        }

        public bool DeleteItem(int contentId)
        {
            EntityModel.Content.Remove(EntityModel.Content.FirstOrDefault(x => x.Id == contentId));
            EntityModel.ContentLocale.Where(x => x.ContentId == contentId).ToList().ForEach(x => EntityModel.ContentLocale.Remove(x));
            EntityModel.SaveChanges();
            return true;
        }

        public bool UpdateOrder(int contentId, bool isDown)
        {
            var contentItem = EntityModel.Content.FirstOrDefault(x => x.Id == contentId);
            if (isDown)
            {
                var isThere = EntityModel.Content.Any(x => x.Order > contentItem.Order && x.CategoryId == contentItem.CategoryId);
                if (!isThere) return false;
                var items = EntityModel.Content.Where(x => x.Order > contentItem.Order && x.CategoryId == contentItem.CategoryId);
                var itemOrder = items.Min(x => x.Order);
                var item = items.FirstOrDefault(x => x.Order == itemOrder && x.CategoryId == contentItem.CategoryId);
                if (contentItem != null)
                {
                    var order = contentItem.Order;
                    if (item != null)
                    {
                        contentItem.Order = item.Order;
                        item.Order = order;
                    }
                }
                EntityModel.SaveChanges();
                return true;
            }
            else
            {
                var isThere = EntityModel.Content.Any(x => x.Order < contentItem.Order && x.CategoryId == contentItem.CategoryId);
                if (!isThere) return false;
                var items = EntityModel.Content.Where(x => x.Order < contentItem.Order && x.CategoryId == contentItem.CategoryId);
                var itemOrder = items.Max(x => x.Order);
                var item = items.FirstOrDefault(x => x.Order == itemOrder && x.CategoryId == contentItem.CategoryId);
                if (contentItem != null)
                {
                    var order = contentItem.Order;
                    if (item != null)
                    {
                        contentItem.Order = item.Order;
                        item.Order = order;
                    }
                }
                EntityModel.SaveChanges();
                return true;
            }
        }

        public void Create(int catId, string title, string desc, string fileTitle, string fileUrl, string imageUrl)
        {
            var content = new Content()
            {
                CreateDate = DateTime.Now,
                CategoryId = catId,
                Order = GetMaxOrder(catId),
                Active = false
            };
            EntityModel.Content.Add(content);
            EntityModel.SaveChanges();
            foreach (var locale in EntityModel.Languages.ToList().Select(item => new ContentLocale()
            {
                ContentId = content.Id,
                Lang = item.Lang,
                Title = title,
                Content = desc,
                FileTitle = fileTitle,
                FileUrl = fileUrl,
                ImageUrl = imageUrl
            }))
            {
                EntityModel.ContentLocale.Add(locale);
            }
            EntityModel.SaveChanges();
        }

        private int GetMaxOrder(int catId)
        {
            if (EntityModel.Content.Any(x => x.CategoryId == catId))
                return EntityModel.Content.Where(x => x.CategoryId == catId).Max(x => x.Order) + 1;
            return 1;
        }

        public IList<ContentDto> GetLocalesByContentId(int contentId)
        {
            var data = (from p in EntityModel.Content
                        join q in EntityModel.ContentLocale on p.Id equals q.ContentId
                        where p.Id == contentId
                        select new ContentDto()
                        {
                            Id = p.Id,
                            CategoryId = p.CategoryId,
                            LocaleId = q.Id,
                            Title = q.Title,
                            FileTitle = q.FileTitle,
                            Lang = q.Lang
                        }).ToList();
            return data;
        }

        public void Update(int id, string title, string content, string fileTitle, string fileUrl, string imageUrl)
        {
            var contentLocale = EntityModel.ContentLocale.FirstOrDefault(x => x.Id == id);
            if (contentLocale == null)
                return;
            contentLocale.Title = title;
            contentLocale.Content = content;
            contentLocale.FileTitle = fileTitle;
            if (!string.IsNullOrWhiteSpace(imageUrl))
                contentLocale.ImageUrl = imageUrl;
            if (!string.IsNullOrWhiteSpace(fileUrl))
                contentLocale.FileUrl = fileUrl;
            EntityModel.SaveChanges();
        }

        public ContentDto GetLocaleById(int id)
        {
            var data = (from p in EntityModel.Content
                        join q in EntityModel.ContentLocale on p.Id equals q.ContentId
                        where q.Id == id
                        select new ContentDto()
                        {
                            Id = p.Id,
                            LocaleId = q.Id,
                            Title = q.Title,
                            Content = q.Content,
                            FileTitle = q.FileTitle,
                            FileUrl = q.FileUrl,
                            ImageUrl = q.ImageUrl,
                            Lang = q.Lang
                        }).FirstOrDefault();
            return data;
        }

        public ContentBannerDto GetBannerLocaleById(int id)
        {
            var data = (from p in EntityModel.ContentBanner
                        where p.Id == id
                        select new ContentBannerDto()
                        {
                            Id = p.Id,
                            ContentId = p.ContentId,
                            ImageUrl = p.ImageUrl,
                            Lang = p.Lang
                        }).FirstOrDefault();
            return data;
        }

        public ContentBannerDto GetBannerLocaleByContentId(int id)
        {
            var data = (from p in EntityModel.ContentBanner
                        where p.ContentId == id && p.Lang==Culture
                        select new ContentBannerDto()
                        {
                            Id = p.Id,
                            ContentId = p.ContentId,
                            ImageUrl = p.ImageUrl,
                            Lang = p.Lang
                        }).FirstOrDefault();
            return data;
        }

        public IList<ContentBannerDto> GetBannerLocalesById(int id)
        {
            var data = (from p in EntityModel.ContentBanner
                        where p.ContentId == id
                        select new ContentBannerDto()
                        {
                            Id = p.Id,
                            ContentId = p.ContentId,
                            ImageUrl = p.ImageUrl,
                            Lang = p.Lang
                        }).ToList();
            return data;
        }

        public void UpdateBanner(int id, string imageUrl)
        {
            var contentLocale = EntityModel.ContentBanner.FirstOrDefault(x => x.Id == id);
            if (contentLocale == null)
                return;
            if (!string.IsNullOrWhiteSpace(imageUrl))
                contentLocale.ImageUrl = imageUrl;
            EntityModel.SaveChanges();
        }
    }
}
