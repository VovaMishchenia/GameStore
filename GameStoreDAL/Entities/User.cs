
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreDAL.Entities
{
    public class User: Microsoft.AspNet.Identity.EntityFramework.IdentityUser
    {
        
        public int? AddressId { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Address Address{ get; set; }
        public User()
        {
            Orders = new List<Order>();
        }
    }
}
