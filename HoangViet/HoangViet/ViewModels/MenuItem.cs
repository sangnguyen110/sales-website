using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoangViet.ViewModels
{
    public class MenuItem
    {
        public MenuItem()
        {
            this.ChildMenuItems = new List<MenuItem>();
        }
        public string Title { get; set; }
        public string Url { get; set; }
        public int CountProduct { get; set; }

        public string Slug { get; set; }

        public virtual ICollection<MenuItem> ChildMenuItems { get; set; }
    }
}