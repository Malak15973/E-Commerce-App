using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Models
{
    public class CartItemVM
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }
        public double Price { get; set; }
    }
}
