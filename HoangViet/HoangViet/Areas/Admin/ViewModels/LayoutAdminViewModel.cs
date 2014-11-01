using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoangViet.Areas.Admin.ViewModels
{
    public class LayoutAdminViewModel
    {
        public string UserName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}