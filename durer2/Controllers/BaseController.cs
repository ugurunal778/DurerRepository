using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facade;
using System.Web.UI.WebControls;
using System.IO;

namespace durer2.Controllers
{
    public class BaseController : Controller
    {
        private PageFacade _pageFacade;
        private UserFacade _userFacade;
        private NewsFacade _newsFacade;
        private ContentFacade _contentFacade;
        private MailFacade _mailFacade;

        public PageFacade PageFacade
        {
            get { return _pageFacade ?? (_pageFacade = new PageFacade()); }
        }

        public UserFacade UserFacade
        {
            get { return _userFacade ?? (_userFacade = new UserFacade()); }
        }

        public NewsFacade NewsFacade
        {
            get { return _newsFacade ?? (_newsFacade = new NewsFacade()); }
        }

        public ContentFacade ContentFacade
        {
            get { return _contentFacade ?? (_contentFacade = new ContentFacade()); }
        }

        public MailFacade MailFacade
        {
            get { return _mailFacade ?? (_mailFacade = new MailFacade()); }
        }

        public string UploadImage(HttpPostedFileBase uploaded)
        {
            var ext = Path.GetExtension(uploaded.FileName);
            var name = Guid.NewGuid() + ext;
            string path = Server.MapPath("/img/res/");
            var src = path + @"\" + name;
            uploaded.SaveAs(src);
            return "~/img/res/" + name;
        }
    }
}