using ElFinder;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace HoangViet.Areas.Admin.Controllers
{
    public class FilesController : BaseAdminController
    {
        private Connector _connector;

        public Connector Connector
        {
            get
            {
                if (_connector == null)
                {
                    FileSystemDriver driver = new FileSystemDriver();
                    DirectoryInfo thumbsStorage = new DirectoryInfo(Server.MapPath("/Files/Thumbnails"));
                    //driver.AddRoot(new Root(new DirectoryInfo(Server.MapPath("~/Files/")), "http://" + Request.Url.Authority + "/Files/")                   
                    //{
                    //    IsLocked = true,
                    //    IsReadOnly = true,
                    //    IsShowOnly = false,
                    //    ThumbnailsStorage = thumbsStorage,
                    //    ThumbnailsUrl = "Thumbnails/"
                    //});
                    driver.AddRoot(new Root(new DirectoryInfo(Server.MapPath("~/Files")), "http://" + Request.Url.Authority + "/Files/")
                    {
                        Alias = "Files",
                        StartPath = new DirectoryInfo(Server.MapPath("~/Files/Images")),
                        ThumbnailsStorage = thumbsStorage,
                        MaxUploadSizeInMb = 2.2,
                        ThumbnailsUrl = "/Thumbnails/"
                    });
                    _connector = new Connector(driver); 
                }
                return _connector;
            }
        }
        public ActionResult Index()
        {
            return Connector.Process(this.HttpContext.Request);
        }

        public ActionResult SelectFile(string target)
        {
            return Json(Connector.GetFileByHash(target).FullName);
        }

        public ActionResult MultiSelectFile(List<string> targets)
        {
            var urlsReponse = new List<string>();
            var driver = new FileSystemDriver();
            DirectoryInfo thumbsStorage = new DirectoryInfo(Server.MapPath("/Files/Thumbnails"));           
            driver.AddRoot(new Root(new DirectoryInfo(Server.MapPath("~/Files")), "http://" + Request.Url.Authority + "/Files/")
            {
                Alias = "Files",
                StartPath = new DirectoryInfo(Server.MapPath("~/Files/Images")),
                ThumbnailsStorage = thumbsStorage,
                MaxUploadSizeInMb = 2.2,
                ThumbnailsUrl = "/Thumbnails/"
            });
            foreach(var target in targets){
                FullPath path = driver.ParsePath(target);
                if (!path.IsDirectoty)
                    urlsReponse.Add("/"+path.Root.Alias+"/"+path.RelativePath.Replace('\\' , '/'));
            }
            return Json(new { files = urlsReponse});
        }

        public ActionResult Thumbs(string tmb)
        {
            return Connector.GetThumbnail(Request, Response, tmb);
        }
    }
}
