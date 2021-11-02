using Dto;
using Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Facade
{
    public class PageFacade : FacadeBase, IPageFacade
    {
        public IList<PageDto> GetAllByParentIdPage(int parentId, bool? isActive)
        
        {
            var data = (from p in EntityModel.Page
                        join q in EntityModel.PageLocale on p.Id equals q.PageId
                        where q.Lang == Culture && p.ParentId == parentId
                        orderby p.Order
                        select new PageDto()
                        {
                            Id = p.Id,
                            Title = q.Title,
                            Active = p.Active,
                            Order = p.Order,
                            Permalink = q.Permalink
                        });
            if (isActive.HasValue)
            {
                data = data.Where(x => x.Active == isActive.Value);
            }
            return data.ToList();
        }

        public IList<PageDto> GetLocalesByPageIdPage(int pageId)
        {
            var data = (from p in EntityModel.Page
                        join q in EntityModel.PageLocale on p.Id equals q.PageId
                        where p.Id == pageId
                        select new PageDto()
                        {
                            Id = p.Id,
                            ParentId = p.ParentId,
                            LocaleId = q.Id,
                            Title = q.Title,
                            ImageUrl = q.ImageUrl,
                            Lang = q.Lang
                        }).ToList();
            return data;
        }

        public List<PageDto> GetProductsPage(bool isDurer)
        {
            var data = (from p in EntityModel.Page
                        join q in EntityModel.PageLocale on p.Id equals q.PageId
                        where q.Lang == Culture && p.ParentId == 51 && p.IsDurer == isDurer && p.Active == true
                        orderby p.Order
                        select new PageDto()
                        {
                            Id = p.Id,
                            ParentId = p.ParentId,
                            Title = q.Title,
                            Active = p.Active,
                            Permalink = q.Permalink,
                            IsDurer = p.IsDurer,
                            HazrefId = p.HazrefId
                        });


            return data.ToList();
        }

        public dynamic GetHazrefProductsPage()
        {
            throw new NotImplementedException();
        }

        public PageDto GetLocaleByIdPage(int id)
        {
            var data = (from p in EntityModel.Page
                        join q in EntityModel.PageLocale on p.Id equals q.PageId
                        where q.Id == id
                        select new PageDto()
                        {
                            Id = p.Id,
                            LocaleId = q.Id,
                            Title = q.Title,
                            Content = q.Content,
                            ImageUrl = q.ImageUrl,
                            Lang = q.Lang
                        }).FirstOrDefault();
            return data;
        }

        public void UpdatePage(int id, string title, string content, string imageUrl)
        {
            var pageLocale = EntityModel.PageLocale.FirstOrDefault(x => x.Id == id);
            if (pageLocale == null)
                return;
            pageLocale.Title = title;
            pageLocale.Content = content;
            pageLocale.Permalink = RemoveAccentPage(title.ToLower());
            if (!string.IsNullOrWhiteSpace(imageUrl))
                pageLocale.ImageUrl = imageUrl;
            EntityModel.SaveChanges();
        }

        public PageDto GetByPermalinkPage(string permalink)
        {
            var data = (from p in EntityModel.Page
                        join q in EntityModel.PageLocale on p.Id equals q.PageId
                        where q.Lang == Culture && q.Permalink == permalink
                        select new PageDto()
                        {
                            Id = q.PageId,
                            LocaleId = q.Id,
                            ParentId = p.ParentId,
                            Permalink = q.Permalink,
                            Title = q.Title,
                            Content = q.Content,
                            ImageUrl = q.ImageUrl
                        }).FirstOrDefault();
            return data;
        }

        public string GetOtherCulturePermalinkPage(string permalink)
        {
            PageLocale pl = EntityModel.PageLocale.Where(x => x.Permalink == permalink).FirstOrDefault();
            string otherPermalink = EntityModel.PageLocale.Where(x => x.PageId == pl.PageId && x.Lang != pl.Lang).FirstOrDefault().Permalink;
            return otherPermalink;
        }

        public PageDto GetFirstByParentIdPage(int parentId)
        {
            var data = (from p in EntityModel.Page
                        join q in EntityModel.PageLocale on p.Id equals q.PageId
                        where q.Lang == Culture && p.ParentId == parentId && p.Active == true
                        orderby p.Order
                        select new PageDto()
                        {
                            Id = p.Id,
                            ParentId = p.ParentId,
                            Permalink = q.Permalink,
                            Title = q.Title,
                            Content = q.Content,
                            ImageUrl = q.ImageUrl
                        }).FirstOrDefault();
            return data;
        }

        public PageDto GetByIdPage(int id)
        {
            var data = (from p in EntityModel.Page
                        join q in EntityModel.PageLocale on p.Id equals q.PageId
                        where q.Lang == Culture && p.Id == id
                        select new PageDto()
                        {
                            Id = p.Id,
                            ParentId = p.ParentId,
                            Permalink = q.Permalink,
                            Title = q.Title,
                            Content = q.Content,
                            ImageUrl = q.ImageUrl
                        }).FirstOrDefault();
            return data;
        }

        public bool hasSubLinksPage(int parentId)
        {
            if (EntityModel.Page.Where(x => x.ParentId == parentId).Count() > 0)
            {
                return true;
            }
            return false;
        }

        public string RemoveAccentPage(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            string str = System.Text.Encoding.ASCII.GetString(bytes);
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-");
            return str;
        }

        public void CreatePage(int parentId, string title, string content, string imageUrl)
        {
            string str = RemoveAccentPage(title.ToLower());

            var page = new Page()
            {
                CreateDate = DateTime.Now,
                ParentId = parentId,
                Order = GetMaxOrderPage(parentId),
                Active = false
            };
            EntityModel.Page.Add(page);
            EntityModel.SaveChanges();
            foreach (var locale in EntityModel.Languages.ToList().Select(item => new PageLocale()
            {
                PageId = page.Id,
                Lang = item.Lang,
                Title = title,
                Content = content,
                Permalink = str,
                ImageUrl = imageUrl
            }))
            {
                if (locale.Lang != "tr-TR")
                {
                    locale.Permalink = str + "-en";
                }
                EntityModel.PageLocale.Add(locale);
            }
            EntityModel.SaveChanges();
        }

        public void UpdateActivePage(int pageId)
        {
            var item = EntityModel.Page.FirstOrDefault(x => x.Id == pageId);
            if (item != null) item.Active = !item.Active;
            EntityModel.SaveChanges();
        }

        public bool DeleteItemPage(int pageId)
        {
            EntityModel.Page.Remove(EntityModel.Page.FirstOrDefault(x => x.Id == pageId));
            EntityModel.PageLocale.Where(x => x.PageId == pageId).ToList().ForEach(x => EntityModel.PageLocale.Remove(x));
            foreach (var item in EntityModel.PageFile.Where(x => x.PageId == pageId))
            {
                EntityModel.PageFileLocale.Where(x => x.PageFileId == item.Id).ToList().ForEach(x => EntityModel.PageFileLocale.Remove(x));
            }
            EntityModel.PageFile.Where(x => x.PageId == pageId).ToList().ForEach(x => EntityModel.PageFile.Remove(x));
            EntityModel.SaveChanges();
            return true;
        }

        public bool UpdateOrderPage(int pageId, bool isDown)
        {
            var pageItem = EntityModel.Page.FirstOrDefault(x => x.Id == pageId);
            if (isDown)
            {
                var isThere = EntityModel.Page.Any(x => x.Order > pageItem.Order && x.ParentId == pageItem.ParentId);
                if (!isThere) return false;
                var items = EntityModel.Page.Where(x => x.Order > pageItem.Order && x.ParentId == pageItem.ParentId);
                var itemOrder = items.Min(x => x.Order);
                var item = items.FirstOrDefault(x => x.Order == itemOrder && x.ParentId == pageItem.ParentId);
                if (pageItem != null)
                {
                    var order = pageItem.Order;
                    if (item != null)
                    {
                        pageItem.Order = item.Order;
                        item.Order = order;
                    }
                }
                EntityModel.SaveChanges();
                return true;
            }
            else
            {
                var isThere = EntityModel.Page.Any(x => x.Order < pageItem.Order && x.ParentId == pageItem.ParentId);
                if (!isThere) return false;
                var items = EntityModel.Page.Where(x => x.Order < pageItem.Order && x.ParentId == pageItem.ParentId);
                var itemOrder = items.Max(x => x.Order);
                var item = items.FirstOrDefault(x => x.Order == itemOrder && x.ParentId == pageItem.ParentId);
                if (pageItem != null)
                {
                    var order = pageItem.Order;
                    if (item != null)
                    {
                        pageItem.Order = item.Order;
                        item.Order = order;
                    }
                }
                EntityModel.SaveChanges();
                return true;
            }
        }

        public IList<PageFileDto> GetFilesByIdPage(int pageId)
        {
            var data = (from p in EntityModel.PageFile
                        join q in EntityModel.PageFileLocale on p.Id equals q.PageFileId
                        where p.PageId == pageId && q.Lang == Culture
                        orderby p.Order
                        select new PageFileDto()
                        {
                            Id = p.Id,
                            LocaleId = q.Id,
                            PageId = p.PageId,
                            Title = q.Title,
                            FileUrl = q.FileUrl,
                            Lang = q.Lang
                        });
            return data.ToList();
        }

        public bool UpdateFileOrderPage(int fileId, bool isDown)
        {
            var fileItem = EntityModel.PageFile.FirstOrDefault(x => x.Id == fileId);
            if (isDown)
            {
                var isThere = EntityModel.PageFile.Any(x => x.Order > fileItem.Order && x.PageId == fileItem.PageId);
                if (!isThere) return false;
                var items = EntityModel.PageFile.Where(x => x.Order > fileItem.Order && x.PageId == fileItem.PageId);
                var itemOrder = items.Min(x => x.Order);
                var item = items.FirstOrDefault(x => x.Order == itemOrder && x.PageId == fileItem.PageId);
                if (fileItem != null)
                {
                    var order = fileItem.Order;
                    if (item != null)
                    {
                        fileItem.Order = item.Order;
                        item.Order = order;
                    }
                }
                EntityModel.SaveChanges();
                return true;
            }
            else
            {
                var isThere = EntityModel.PageFile.Any(x => x.Order < fileItem.Order && x.PageId == fileItem.PageId);
                if (!isThere) return false;
                var items = EntityModel.PageFile.Where(x => x.Order < fileItem.Order && x.PageId == fileItem.PageId);
                var itemOrder = items.Max(x => x.Order);
                var item = items.FirstOrDefault(x => x.Order == itemOrder && x.PageId == fileItem.PageId);
                if (fileItem != null)
                {
                    var order = fileItem.Order;
                    if (item != null)
                    {
                        fileItem.Order = item.Order;
                        item.Order = order;
                    }
                }
                EntityModel.SaveChanges();
                return true;
            }
        }

        public bool DeleteFilePage(int fileId)
        {
            EntityModel.PageFile.Remove(EntityModel.PageFile.FirstOrDefault(x => x.Id == fileId));
            EntityModel.PageFileLocale.Where(x => x.PageFileId == fileId).ToList().ForEach(x => EntityModel.PageFileLocale.Remove(x));
            EntityModel.SaveChanges();
            return true;
        }

        public void CreateFilePage(int pageId, string fileUrl, string title)
        {
            var pageFile = new PageFile()
            {
                CreateDate = DateTime.Now,
                PageId = pageId,
                Order = GetMaxFileOrderPage(pageId)
            };
            EntityModel.PageFile.Add(pageFile);
            EntityModel.SaveChanges();
            foreach (var locale in EntityModel.Languages.ToList().Select(item => new PageFileLocale()
            {
                PageFileId = pageFile.Id,
                Lang = item.Lang,
                Title = title,
                FileUrl = fileUrl
            }))
            {
                EntityModel.PageFileLocale.Add(locale);
            }
            EntityModel.SaveChanges();
        }

        public IList<PageFileDto> GetFileLocalesByIdPage(int fileId)
        {
            var data = (from p in EntityModel.PageFile
                        join q in EntityModel.PageFileLocale on p.Id equals q.PageFileId
                        where p.Id == fileId
                        select new PageFileDto()
                        {
                            Id = p.Id,
                            PageId = p.PageId,
                            LocaleId = q.Id,
                            Title = q.Title,
                            Lang = q.Lang
                        }).ToList();
            return data;
        }

        public void UpdateFilePage(int id, string fileUrl, string title)
        {
            var pageLocale = EntityModel.PageFileLocale.FirstOrDefault(x => x.Id == id);
            if (pageLocale == null)
                return;
            pageLocale.Title = title;
            if (!string.IsNullOrWhiteSpace(fileUrl))
                pageLocale.FileUrl = fileUrl;
            EntityModel.SaveChanges();
        }

        public PageFileDto GetFileLocaleByIdPage(int id)
        {
            var data = (from p in EntityModel.PageFile
                        join q in EntityModel.PageFileLocale on p.Id equals q.PageFileId
                        where q.Id == id
                        select new PageFileDto()
                        {
                            Id = p.Id,
                            LocaleId = q.Id,
                            PageId = p.PageId,
                            Title = q.Title,
                            FileUrl = q.FileUrl,
                            Lang = q.Lang
                        }).FirstOrDefault();
            return data;
        }

        public IList<PageDto> GetPageLinksByParentIdPage(int parentId, bool? isActive)
        {
            var data = (from p in EntityModel.Page
                        join q in EntityModel.PageLocale on p.Id equals q.PageId
                        where q.Lang == Culture && p.ParentId == parentId
                        orderby p.Order
                        select new PageDto()
                        {
                            Id = p.Id,
                            ParentId = p.ParentId,
                            Title = q.Title,
                            Active = p.Active,
                            Permalink = q.Permalink,
                            IsDurer = p.IsDurer,
                            HazrefId = p.HazrefId
                        });
            if (isActive.HasValue)
                data = data.Where(x => x.Active == isActive.Value);

            return data.ToList();
        }

        public int GetMaxOrderPage(int parentId)
        {
            if (EntityModel.Page.Any(x => x.ParentId == parentId))
                return EntityModel.Page.Where(x => x.ParentId == parentId).Max(x => x.Order) + 1;
            return 1;
        }

        public int GetMaxFileOrderPage(int pageId)
        {
            if (EntityModel.PageFile.Any(x => x.PageId == pageId))
                return EntityModel.PageFile.Where(x => x.PageId == pageId).Max(x => x.Order) + 1;
            return 1;
        }
    }
}