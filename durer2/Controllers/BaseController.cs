using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.IO;
using durer2.ServiceReference;

namespace durer2.Controllers
{
    public class BaseController : Controller
    {
        private WcfServiceClient _projectService;

        public WcfServiceClient ProjectService
        {
            //get
            //{
            //    if (_projectService is null)
            //    {
            //        _projectService = new WcfServiceClient();
            //    }
            //    return _projectService;
            //}
            get { return _projectService ?? (_projectService = new WcfServiceClient()); }
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