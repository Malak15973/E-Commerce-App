using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.DAL.Entites
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(40)]
        public string Title { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [MinLength(0)]
        public double Price { get; set; }
        public string PhotoPath { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
