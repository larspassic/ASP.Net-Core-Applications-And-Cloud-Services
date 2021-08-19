using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ziggle.WebSite.Models
{
    public class ShoppingCartItem
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        
    }
}
