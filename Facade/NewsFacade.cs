using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    public class NewsFacade : FacadeBase
    {

        public IList<NewsDto> GetAll(bool? isActive)
        {
            var data = (from p in EntityModel.News
                        join q in EntityModel.NewsLocale on p.Id equals q.NewsId
                        where q.Lang == Culture
                        orderby p.Order
                        select new NewsDto()
                        {
                            Id = p.Id,
                            Title = q.Title,
                            Content = q.Content,
                            Active = p.Active,
                            CreateDate = p.CreateDate,
                        });
            if (isActive.HasValue)
                data = data.Where(x => x.Active == isActive.Value);
            return data.ToList();
        }

        public IList<NewsImageDto> GetAllImages()
        {
            var data = (from p in EntityModel.NewsImage
                        select new NewsImageDto()
                        {
                            Id = p.Id,
                            NewsId = p.NewsId,
                            ImageUrl = p.ImageUrl,
                            CreateDate = p.CreateDate,
                        });
            return data.ToList();
        }

        public void UpdateActive(int newsId)
        {
            var item = EntityModel.News.FirstOrDefault(x => x.Id == newsId);
            if (item != null) item.Active = !item.Active;
            EntityModel.SaveChanges();
        }

        public bool UpdateOrder(int newsId, bool isDown)
        {
            var newsItem = EntityModel.News.FirstOrDefault(x => x.Id == newsId);
            if (isDown)
            {
                var isThere = EntityModel.News.Any(x => x.Order > newsItem.Order);
                if (!isThere) return false;
                var items = EntityModel.News.Where(x => x.Order > newsItem.Order);
                var itemOrder = items.Min(x => x.Order);
                var item = items.FirstOrDefault(x => x.Order == itemOrder);
                if (newsItem != null)
                {
                    var order = newsItem.Order;
                    if (item != null)
                    {
                        newsItem.Order = item.Order;
                        item.Order = order;
                    }
                }
                EntityModel.SaveChanges();
                return true;
            }
            else
            {
                var isThere = EntityModel.News.Any(x => x.Order < newsItem.Order);
                if (!isThere) return false;
                var items = EntityModel.News.Where(x => x.Order < newsItem.Order);
                var itemOrder = items.Max(x => x.Order);
                var item = items.FirstOrDefault(x => x.Order == itemOrder);
                if (newsItem != null)
                {
                    var order = newsItem.Order;
                    if (item != null)
                    {
                        newsItem.Order = item.Order;
                        item.Order = order;
                    }
                }
                EntityModel.SaveChanges();
                return true;
            }
        }


        private int GetMaxOrder()
        {
            if (EntityModel.News.Any())
                return EntityModel.News.Max(x => x.Order) + 1;
            return 1;
        }

        public bool DeleteItem(int newsId)
        {
            EntityModel.News.Remove(EntityModel.News.FirstOrDefault(x => x.Id == newsId));
            EntityModel.NewsLocale.Where(x => x.NewsId == newsId).ToList().ForEach(x => EntityModel.NewsLocale.Remove(x));
            EntityModel.NewsImage.Where(x => x.NewsId == newsId).ToList().ForEach(x => EntityModel.NewsImage.Remove(x));
            EntityModel.SaveChanges();
            return true;
        }

        public IList<NewsDto> GetLocalesById(int id)
        {
            var data = (from p in EntityModel.News
                        join q in EntityModel.NewsLocale on p.Id equals q.NewsId
                        where p.Id == id
                        select new NewsDto()
                        {
                            Id = p.Id,
                            LocaleId = q.Id,
                            Title = q.Title,
                            Lang = q.Lang
                        }).ToList();

            return data;
        }

        public NewsDto GetLocaleById(int id)
        {
            var data = (from p in EntityModel.News
                        join q in EntityModel.NewsLocale on p.Id equals q.NewsId
                        where q.Id == id
                        select new NewsDto()
                        {
                            Id = p.Id,
                            LocaleId = q.Id,
                            Title = q.Title,
                            Content = q.Content,
                            CreateDate = p.CreateDate
                        }).FirstOrDefault();

            return data;
        }

        public void Update(int id, string title, string content, DateTime date)
        {
            var newsLocale = EntityModel.NewsLocale.FirstOrDefault(x => x.Id == id);
            if (newsLocale == null)
                return;
            var news = EntityModel.News.FirstOrDefault(x=>x.Id==newsLocale.NewsId);
            news.CreateDate = date;
            newsLocale.Title = title;
            newsLocale.Content = content;
            EntityModel.SaveChanges();
        }

        public void Create(string title, string content, DateTime date)
        {
            var news = new News()
            {
                CreateDate = date,
                Order = GetMaxOrder(),
                Active = false
            };

            EntityModel.News.Add(news);
            EntityModel.SaveChanges();


            foreach (var locale in EntityModel.Languages.ToList().Select(item => new NewsLocale()
            {
                NewsId = news.Id,
                Lang = item.Lang,
                Content = content,
                Title = title
            }))
            {
                EntityModel.NewsLocale.Add(locale);
            }
            EntityModel.SaveChanges();

        }

        public NewsDto GetById(int id, bool? isActive)
        {
            var data = (from p in EntityModel.News
                        join q in EntityModel.NewsLocale on p.Id equals q.NewsId
                        where q.Lang == Culture && p.Id==id && p.Active==true
                        select new NewsDto()
                        {
                            Id = p.Id,
                            Title = q.Title,
                            Content = q.Content,
                            CreateDate = p.CreateDate,
                        });
            if (isActive.HasValue)
            {
                data = data.Where(x=>x.Active==isActive.Value);
            }
            return data.FirstOrDefault();
        }

        public IList<NewsImageDto> GetImagesById(int id)
        {
            var data = (from p in EntityModel.NewsImage
                        where p.NewsId == id
                        select new NewsImageDto()
                        {
                            Id = p.Id,
                            ImageUrl = p.ImageUrl
                        }).ToList();
            return data;
        }

        public NewsImageDto GetImageById(int id)
        {
            var data = (from p in EntityModel.NewsImage
                        where p.Id == id
                        select new NewsImageDto()
                        {
                            Id = p.Id,
                            NewsId = p.NewsId,
                            ImageUrl = p.ImageUrl,
                        }).FirstOrDefault();
            return data;
        }

        public void CreateImage(int newsId, string imageUrl)
        {
            var newsImage = new NewsImage()
            {
                CreateDate = DateTime.Now,
                ImageUrl = imageUrl,
                NewsId = newsId
            };
            EntityModel.NewsImage.Add(newsImage);
            EntityModel.SaveChanges();
        }

        public bool DeleteImage(int imageId)
        {
            EntityModel.NewsImage.Remove(EntityModel.NewsImage.FirstOrDefault(x => x.Id == imageId));
            EntityModel.SaveChanges();
            return true;
        }

        public void UpdateImage(int id, string imageUrl)
        {
            var newsImage = EntityModel.NewsImage.FirstOrDefault(x => x.Id == id);
            if (newsImage == null)
                return;
            newsImage.ImageUrl = imageUrl;
            EntityModel.SaveChanges();
        }
    }
}
