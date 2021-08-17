using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Models
{
    public class RateVM
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int UserRate { get; set; }
    }
}
