using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HoangViet.ViewModels
{
    public class LayoutViewModel
    {
        public LayoutViewModel()
        {
            this.MenuItems = new List<MenuItem>();
            LoginViewModel = new Accounts.LoginViewModel();
        }
        public virtual SelectList TopLevelCategory { get; set; }
        public bool IsAuthenticated { get; set; }

        public bool IsAdmin { get; set; }

        public string UserName { get; set; }

        public HoangViet.ViewModels.Accounts.LoginViewModel LoginViewModel { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }

        public string SelectedMenuItem { get; set; }

        public string CartTotal { get; set; }

        public int CartCount { get; set; }
    }
}