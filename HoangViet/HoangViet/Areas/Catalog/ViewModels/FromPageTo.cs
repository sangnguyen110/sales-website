using HoangViet.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoangViet.Areas.Catalog.ViewModels
{
    public class FromPageTo
    {
        public int EntityId { get; set; }
        public FromPageEnum FromPage { get; set; }
    }
}