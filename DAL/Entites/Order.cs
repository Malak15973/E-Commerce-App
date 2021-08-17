using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.DAL.Entites
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
