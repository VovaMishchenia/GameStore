using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreDAL.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        //[Required]
        public string Country { get; set; }
        //[Required]
        public string City { get; set; }
        //[Required]
        public string Street { get; set; }
       // [Required]
        public int Building { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
        public Address()
        {
            Users = new List<User>();
        }
    }
}
