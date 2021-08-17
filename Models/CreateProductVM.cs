using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Models
{
    public class CreateProductVM
    {
        [Required]
        [StringLength(40)]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string PhotoPath { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
