using HoangViet.Models.Orders;
using System.Collections.Generic;

namespace HoangViet.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartItem> CartItems { get; set; }
        public string CartTotal { get; set; }

        public string CartTax { get; set; }

        public string CartTotalIncTax { get; set; }

        public int CartCount { get; set; }
    }
}