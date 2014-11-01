using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoangViet.Areas.Admin.Controllers
{
    public class ElFinderController : BaseAdminController
    {
        //
        // GET: /Index/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CkEditor()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }
    }
}
