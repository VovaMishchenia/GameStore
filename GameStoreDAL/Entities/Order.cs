using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreDAL.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Game> Games { get; set; }

        public Order()
        {
            Games = new List<Game>();
        }
    }
}
