using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.DAL.Entites
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
